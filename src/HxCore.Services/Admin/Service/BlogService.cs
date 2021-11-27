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
            if (blogModel.IsPublish)
            {
                entity.PublishDate = DateTime.Now;
            }
            entity.PureContent = HtmlHelper.FilterHtml(blogModel.Content, 1000);
            var addBlogTagIds = await AddBlogTags(blogModel.PersonTags);
            entity.BlogTags = string.Join(",", addBlogTagIds);
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
            if (blogModel.IsPublish)
            {
                entity.PublishDate = DateTime.Now;
            }
            entity.PureContent = HtmlHelper.FilterHtml(blogModel.Content, 1000);
            //再添加
            var addBlogTagIds = await AddBlogTags(blogModel.PersonTags);
            entity.BlogTags = string.Join(",", addBlogTagIds);
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
        /// 添加博客标签
        /// </summary>
        /// <param name="personTags"></param>
        /// <returns></returns>
        private async Task<List<string>> AddBlogTags(List<BlogManagePersonTag> personTags)
        {
            List<string> addBlogTagIds = new List<string>();
            if (personTags == null || personTags.Count == 0) return addBlogTagIds;
            List<T_TagInfo> addTagEntityList = new List<T_TagInfo>();
            var tagRepository = this.Repository.Change<T_TagInfo>();
            personTags.ForEach(p =>
            {
                if (!string.IsNullOrEmpty(p.Name))
                {
                    var tag = tagRepository.FirstOrDefaultAsync(r => r.Name == p.Name.Trim()
                            && r.CreaterId == UserContext.UserId).Result;
                    if (tag == null)
                    {
                        tag = new T_TagInfo
                        {
                            Id = Helper.GetSnowId(),
                            Name = p.Name
                        };
                        tag.SetCreater(UserContext.UserId, UserContext.UserName);
                        addTagEntityList.Add(tag);
                    }
                    addBlogTagIds.Add(tag.Id);
                }
            });
            if (addTagEntityList.Count > 0)
            {
                await tagRepository.InsertAsync(addTagEntityList);
            }
            return addBlogTagIds;
        }
        #endregion
    }

}
