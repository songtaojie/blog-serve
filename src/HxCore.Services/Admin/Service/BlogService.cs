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

namespace HxCore.Services
{
    /// <summary>
    /// 博客的服务类
    /// </summary>
    public class BlogService : BaseStatusService<T_Blog>, IBlogService
    {
        private IRepository<T_BlogExtend> ExtendRepository { get; }
        public BlogService(IRepository<T_Blog,MasterDbContextLocator> dal, IRepository<T_BlogExtend> extendRepository)
            : base(dal)
        {
            this.ExtendRepository = extendRepository;
        }
        #region 新增编辑
        public async Task<bool> InsertAsync(BlogManageCreateModel blogModel)
        {
            var entity = this.Mapper.Map<T_Blog>(blogModel);
            if (blogModel.IsPublish) entity.PublishDate = DateTime.Now;
            entity.PureContent = HtmlHelper.FilterHtml(blogModel.Content, 100);
            if (blogModel.BlogTags != null && blogModel.BlogTags.Any())
            {
                entity.BlogTags = string.Join(",", blogModel.BlogTags);
            }
            this.BeforeInsert(entity);
            // 扩展表
            var blogExtend = new T_BlogExtend
            {
                Id = entity.Id,
                Content = blogModel.Content,
                ContentHtml = blogModel.ContentHtml
            };
            await this.ExtendRepository.InsertAsync(blogExtend);
            await this.Repository.InsertAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BlogManageCreateModel blogModel)
        {
            if (string.IsNullOrEmpty(blogModel.Id)) throw new UserFriendlyException("无效的标识",ErrorCodeEnum.ParamsNullError);
            var entity = await this.FindAsync(blogModel.Id);
            var extendEntity = await this.ExtendRepository.FindAsync(blogModel.Id);
            if (entity == null && extendEntity == null) throw new UserFriendlyException("文章不存在",ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(blogModel, entity);
            entity.BlogTags = String.Empty;
            if (blogModel.BlogTags != null && blogModel.BlogTags.Any())
            {
                entity.BlogTags = string.Join(",", blogModel.BlogTags);
            }
            if (blogModel.IsPublish) entity.PublishDate = DateTime.Now;
            entity.PureContent = HtmlHelper.FilterHtml(blogModel.Content, 100);
            this.BeforeUpdate(entity);
            //扩展表
            extendEntity.Content = blogModel.Content;
            extendEntity.ContentHtml = blogModel.ContentHtml;
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
                tag.SetCreater(UserContext.UserId, UserContext.UserName);
                tag.SetDisable(tagModel.IsEnabled? StatusEntityEnum.No:StatusEntityEnum.Yes, UserContext.UserId, UserContext.UserName);
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
                tag.SetDisable(tagModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserContext.UserId, UserContext.UserName);
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
                category.SetCreater(UserContext.UserId, UserContext.UserName);
                category.SetDisable(categoryModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserContext.UserId, UserContext.UserName);
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
                category.SetDisable(categoryModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserContext.UserId, UserContext.UserName);
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
            await this.Repository.UpdateIncludeAsync(entity, new string[] { "ReadCount" });
            return await this.Repository.SaveNowAsync() > 1;
        }
        #endregion
    }

}
