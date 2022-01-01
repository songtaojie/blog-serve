using HxCore.IServices;
using HxCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hx.Sdk.Common.Helper;
using HxCore.Model.Admin.Blog;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.FriendlyException;
using HxCore.Enums;
using System.Linq;
using HxCore.Model.NotificationHandlers;
using Hx.Sdk.Core;
using HxCore.IServices.Elastic;

namespace HxCore.Services
{
    /// <summary>
    /// 博客的服务类
    /// </summary>
    public class BlogService : BaseStatusService<T_Blog>, IBlogService
    {
        private IRepository<T_BlogExtend> ExtendRepository { get; }
        public BlogService(IRepository<T_Blog,MasterDbContextLocator> dal, IRepository<T_BlogExtend> extendRepository, IElasticClientProvider provider)
            : base(dal, provider)
        {
            this.ExtendRepository = extendRepository;
        }
        #region 新增编辑
        public async Task<bool> InsertAsync(BlogManageCreateModel blogModel)
        {
            var entity = this.Mapper.Map<T_Blog>(blogModel);
            if (blogModel.IsPublish) entity.PublishDate = DateTime.Now;
            entity.PureContent = HtmlHelper.FilterHtml(blogModel.Content, 100);
           
            this.BeforeInsert(entity);
            // 扩展表
            var blogExtend = new T_BlogExtend
            {
                Id = entity.Id,
                Content = blogModel.Content,
                ContentHtml = blogModel.ContentHtml
            };
            // 博客标签
            if (blogModel.BlogTags != null && blogModel.BlogTags.Any())
            {
                blogModel.BlogTags = blogModel.BlogTags.Distinct().ToList();
                var tagRepository = this.Repository.Change<T_BlogTag>();
                var tags = new List<T_BlogTag>();
                blogModel.BlogTags.ForEach(tagId =>
                {
                    tags.Add(new T_BlogTag
                    {
                        Id = Helper.GetSnowId(),
                        BlogId = entity.Id,
                        TagId = tagId
                    });
                });
                await tagRepository.InsertAsync(tags);
            }
            // 友情链接
            await AddTimeLine(entity);
            await this.ExtendRepository.InsertAsync(blogExtend);
            await this.Repository.InsertAsync(entity);
            var result = await this.Repository.SaveNowAsync() > 0;
            if (result) AddIntoElastic(entity);
            return result;
        }
        /// <summary>
        /// 添加数据到elastic
        /// </summary>
        /// <param name="entity"></param>
        private void AddIntoElastic(T_Blog entity)
        {
            var elasticList = new List<Model.Client.BlogQueryModel>
            {
                this.Mapper.Map<Model.Client.BlogQueryModel>(entity)
            };
            this.ElasticInsert(elasticList);
        }

        /// <summary>
        /// 添加时间轴
        /// </summary>
        /// <param name="blogModel"></param>
        private async Task AddTimeLine(T_Blog blogEntity)
        {
            var tlRepository = this.Repository.Change<T_TimeLine>();
            var link = string.Format("{0}/article/{1}", AppSettings.GetConfig("Domain"), blogEntity.Id);
            var timeLine = new T_TimeLine
            {
                Id = Helper.GetSnowId(),
                Content = $"发布文章《{blogEntity.Title}》",
                Link = link,
                Target = "_blank"
            };
            timeLine.SetCreater(UserId, UserName);
            await tlRepository.InsertAsync(timeLine);
        }
        public async Task<bool> UpdateAsync(BlogManageCreateModel blogModel)
        {
            if (string.IsNullOrEmpty(blogModel.Id)) throw new UserFriendlyException("无效的标识",ErrorCodeEnum.ParamsNullError);
            var entity = await this.FindAsync(blogModel.Id);
            var extendEntity = await this.ExtendRepository.FindAsync(blogModel.Id);
            if (entity == null && extendEntity == null) throw new UserFriendlyException("文章不存在",ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(blogModel, entity);
            if (blogModel.IsPublish && !entity.PublishDate.HasValue) entity.PublishDate = DateTime.Now;
            entity.PureContent = HtmlHelper.FilterHtml(blogModel.Content, 100);
            this.BeforeUpdate(entity);
            //扩展表
            extendEntity.Content = blogModel.Content;
            extendEntity.ContentHtml = blogModel.ContentHtml;
            // 博客标签,先删除再添加
            this.Repository.SqlNonQuery("delete from T_BlogTag where BlogId=@BlogId", new Dictionary<string, object> { { "BlogId", entity.Id } });
            if (blogModel.BlogTags != null && blogModel.BlogTags.Any())
            {
                blogModel.BlogTags = blogModel.BlogTags.Distinct().ToList();
                var tagRepository = this.Repository.Change<T_BlogTag>();
                var tags = new List<T_BlogTag>();
                blogModel.BlogTags.ForEach(tagId =>
                {
                    tags.Add(new T_BlogTag
                    {
                        Id = Helper.GetSnowId(),
                        BlogId = entity.Id,
                        TagId = tagId
                    });
                });
                await tagRepository.InsertAsync(tags);
            }
            await this.ExtendRepository.UpdateIncludeAsync(extendEntity, new string[] { "Content", "ContentHtml" });
            await this.Repository.UpdateAsync(entity);
            await this.Repository.SaveNowAsync();
            return true;
        }

        /// <summary>
        /// 添加或者更新标签
        /// </summary>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        public async Task<bool> AddOrUpdateTagAsync(TagManageModel tagModel)
        {
            var tagRepository = this.Repository.Change<T_TagInfo>();
            if (string.IsNullOrEmpty(tagModel.Id))
            {
                var tag = await tagRepository.FirstOrDefaultAsync(r => r.Name == tagModel.Name.Trim());
                if (tag != null) throw new UserFriendlyException($"已存在名称为【{tagModel.Name}】的标签", ErrorCodeEnum.AddError);
                tag = new T_TagInfo
                {
                    Id = Helper.GetSnowId(),
                    Name = tagModel.Name,
                    BGColor = tagModel.BGColor,
                    OrderSort = tagModel.OrderSort
                };
                tag.SetCreater(UserId, UserName);
                tag.SetDisable(tagModel.IsEnabled? StatusEntityEnum.No:StatusEntityEnum.Yes, UserId, UserName);
                await tagRepository.InsertAsync(tag);
            }
            else
            {
                var isExist = await tagRepository.AnyAsync(r => r.Name == tagModel.Name.Trim() && r.Id != tagModel.Id);
                if (isExist) throw new UserFriendlyException($"已存在名称为【{tagModel.Name}】的标签", ErrorCodeEnum.UpdateError);
                var tag = await tagRepository.FindAsync(tagModel.Id);
                if(tag == null) throw new UserFriendlyException("标签不存在", ErrorCodeEnum.DataNull);
                tag.Name = tagModel.Name;
                tag.BGColor = tagModel.BGColor;
                tag.OrderSort = tagModel.OrderSort;
                tag.SetDisable(tagModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserId, UserName);
                await tagRepository.UpdateAsync(tag);
            }
            return await tagRepository.SaveNowAsync() > 0;
        }

        /// <summary>
        /// 添加或者更新栏目
        /// </summary>
        /// <param name="categoryModel"></param>
        /// <returns></returns>
        public async Task<bool> AddOrUpdateCategoryAsync(CategoryManageModel categoryModel)
        {
            var categoryRepository = this.Repository.Change<T_Category>();
            if (string.IsNullOrEmpty(categoryModel.Id))
            {
                var category = await categoryRepository.FirstOrDefaultAsync(r => r.Name == categoryModel.Name.Trim());
                if (category != null) throw new UserFriendlyException($"已存在名称为【{categoryModel.Name}】的栏目", ErrorCodeEnum.AddError);
                category = new T_Category
                {
                    Id = Helper.GetSnowId(),
                    Name = categoryModel.Name,
                    Description = categoryModel.Description,
                    OrderSort = categoryModel.OrderSort
                };
                category.SetCreater(UserId, UserName);
                category.SetDisable(categoryModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserId, UserName);
                await categoryRepository.InsertAsync(category);
            }
            else
            {
                var isExist = await categoryRepository.AnyAsync(r => r.Name == categoryModel.Name.Trim() && r.Id != categoryModel.Id);
                if (isExist) throw new UserFriendlyException($"已存在名称为【{categoryModel.Name}】的栏目", ErrorCodeEnum.UpdateError);
                var category = await categoryRepository.FindAsync(categoryModel.Id);
                if (category == null) throw new UserFriendlyException("栏目不存在", ErrorCodeEnum.DataNull);
                category.Name = categoryModel.Name;
                category.Description = categoryModel.Description;
                category.OrderSort = categoryModel.OrderSort;
                category.SetDisable(categoryModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserId, UserName);
                await categoryRepository.UpdateAsync(category);
            }
            return await categoryRepository.SaveNowAsync() > 0;
        }

        public async Task<bool> DeleteTagAsync(string tagId)
        {
            var tagRepository = this.Repository.Change<T_TagInfo>();
            await tagRepository.DeleteNowAsync(tagId);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(string categoryId)
        {
            var tagRepository = this.Repository.Change<T_Category>();
            await tagRepository.DeleteNowAsync(categoryId);
            return true;
        }

        public async Task<bool> UpdateBlogReadAsync(UpdateBlogReadModel model)
        {
            if (string.IsNullOrEmpty(model.Id)) throw new UserFriendlyException("无效的标识", ErrorCodeEnum.ParamsNullError);
            var entity = await this.FindAsync(model.Id);
            if (entity == null) throw new UserFriendlyException("文章不存在", ErrorCodeEnum.DataNull);
            entity.ReadCount += 1;
            entity.OrderFactor+=1;
            await this.Repository.UpdateIncludeAsync(entity, new string[] { "ReadCount", "OrderFactor" });
            return await this.Repository.SaveNowAsync() > 1;
        }
        #endregion
    }

}
