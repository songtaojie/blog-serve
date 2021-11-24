using HxCore.IServices;
using HxCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HxCore.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hx.Sdk.Common.Helper;
using Hx.Sdk.Entity.Page;
using Hx.Sdk.Entity;
using HxCore.Model.Admin.Blog;
using HxCore.Entity;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.Extensions;
using Hx.Sdk.Attributes;
using Hx.Sdk.Core;
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
                BlogId = entity.Id,
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
            List<T_BlogTag> addTagEntityList = new List<T_BlogTag>();
            var tagRepository = this.Repository.Change<T_BlogTag>();
            personTags.ForEach(p =>
            {
                if (!string.IsNullOrEmpty(p.Name))
                {
                    var tag = tagRepository.FirstOrDefaultAsync(r => r.Name == p.Name.Trim()
                            && r.CreaterId == UserContext.UserId).Result;
                    if (tag == null)
                    {
                        tag = new T_BlogTag
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

        #region 后台管理-查询
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        public Task<PageModel<BlogManageQueryModel>> QueryBlogListAsync(BlogManageQueryParam param)
        {
            var query = from b in this.Repository.DetachedEntities
                        where b.Deleted == ConstKey.No
                        orderby b.PublishDate descending,b.CreateTime descending
                        select new BlogManageQueryModel
                        {
                            Id = b.Id.ToString(),
                            Publish = b.Publish,
                            ReadCount = b.ReadCount,
                            Title = b.Title,
                            CmtCount = b.CmtCount,
                            CreateTime = b.CreateTime,
                            PublishDate = b.PublishDate,
                            Creater = b.Creater,
                            isMarkDown = b.MarkDown == ConstKey.Yes
                        };
            return query.ToOrderAndPageListAsync(param);
        }

        public async Task<BlogManageDetailModel> GetDetailAsync(string id)
        {
            var detailModel = await (from b in this.Repository.DetachedEntities
                                     join be in this.ExtendRepository.DetachedEntities on b.Id equals be.BlogId
                                     where b.Id == id && b.Deleted == ConstKey.No
                                     select Mapper.Map(b,new BlogManageDetailModel
                                     { 
                                        Content = be.Content
                                     }))
                                   .FirstOrDefaultAsync();
            if (detailModel == null) throw new UserFriendlyException("该文章不存在",ErrorCodeEnum.DataNull);
            if (!string.IsNullOrEmpty(detailModel.BlogTags))
            {
                var tagRepository = this.Repository.Change<T_BlogTag>();
                var blogtagIds = detailModel.BlogTags.Split(",").ToArray();
                detailModel.PersonTags = await tagRepository.Where(t => blogtagIds.Contains(t.Id))
                    .AsNoTracking()
                    .Select(t => new BlogManagePersonTag
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToListAsync();
            }
            return detailModel;
        }
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<BlogManagePersonTag>> GetTagListAsync()
        {
            var tagRepository = this.Repository.Change<T_BlogTag>();
            return await tagRepository.Where(t => t.CreaterId == UserContext.UserId)
                .AsNoTracking()
                .Select(t => new BlogManagePersonTag
                {
                    Id = t.Id.ToString(),
                    Name = t.Name
                }).ToListAsync();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        [CacheData(AbsoluteExpiration = 5)]
        public Task<PageModel<Model.BlogQueryModel>> QueryBlogsAsync(Model.BlogQueryParam param)
        {
            IWebManager webManager = App.GetRequiredService<IWebManager>();
            //var host = AppSettings.Get("ImageOption", "BaseUrl");
            var query = from b in this.Repository.DetachedEntities
                        join u in this.Repository.Context.Set<T_User>().AsNoTracking() on b.CreaterId equals u.Id
                        where b.Publish == ConstKey.Yes
                        && b.Deleted == ConstKey.No
                        && u.Deleted == ConstKey.No
                        select new Model.BlogQueryModel
                        {
                            Id = b.Id.ToString(),
                            NickName = u.NickName,
                            UserName = u.UserName,
                            Title = b.Title,
                            PureContent = b.PureContent,
                            ReadCount = b.ReadCount,
                            CmtCount = b.CmtCount,
                            PublishDate = b.PublishDate,
                            CoverImgUrl = b.CoverImgUrl,
                            AvatarUrl = u.AvatarUrl,
                            MarkDown = b.MarkDown
                        };
            if (string.IsNullOrEmpty(param.SortKey))
            {
                param.SortKey = nameof(Model.BlogQueryModel.PublishDate);
                param.SortType = SortTypeEnum.DESC;
            }
            return query.ToOrderAndPageListAsync(param);
        }

        [CacheData(AbsoluteExpiration = 5)]
        public async Task<BlogDetailModel> FindById(string id)
        {
            var blogModel = await (from b in this.Repository.DetachedEntities
                                   join be in this.ExtendRepository.DetachedEntities on b.Id equals be.BlogId
                                   join u in Db.Set<T_User>() on b.CreaterId equals u.Id
                                   join c in Db.Set<T_Category>() on b.CategoryId equals c.Id into c_temp
                                   from c in c_temp.DefaultIfEmpty()
                                   where b.Id == id
                                   select new BlogDetailModel
                                   {
                                       Id = b.Id,
                                       Title = b.Title,
                                       Publish = b.Publish,
                                       PublishDate = b.PublishDate,
                                       Content = be.Content,
                                       ReadCount = b.ReadCount,
                                       CmtCount = b.CmtCount,
                                       AvatarUrl = u.AvatarUrl,
                                       UserId = u.Id,
                                       UserName = u.UserName,
                                       NickName = u.NickName,
                                       CategoryName = c.Name,
                                       MarkDown = b.MarkDown,
                                       CreateTime = b.CreateTime
                                   }).FirstOrDefaultAsync();
            if (blogModel == null || blogModel.Publish == ConstKey.No) throw new UserFriendlyException("找不到您访问的页面",ErrorCodeEnum.DataNull);
            //获取上一个和下一个博客
            //await GetPreBlogInfo(blogModel);
            //await GetNextBlogInfo(blogModel);
            return blogModel;
        }
        private async Task GetPreBlogInfo(BlogDetailModel blogModel)
        {
            var preBlog = await this.Repository.Where(b => b.CreaterId == blogModel.UserId && b.CreateTime < blogModel.CreateTime)
                    .OrderByDescending(b => b.CreateTime)
                    .FirstOrDefaultAsync();
            if (preBlog != null)
            {
                blogModel.PreId = preBlog.Id.ToString();
                blogModel.PreTitle = preBlog.Title;
            }
        }

        private async Task GetNextBlogInfo(BlogDetailModel blogModel)
        {
            var nextBlog = await this.Repository.Where(b => b.CreaterId == blogModel.UserId && b.CreateTime > blogModel.CreateTime)
                .OrderBy(b => b.CreateTime)
                .FirstOrDefaultAsync();
            if (nextBlog != null)
            {
                blogModel.NextId = nextBlog.Id.ToString();
                blogModel.NextTitle = nextBlog.Title;
            }
        }
        #endregion
    }

}
