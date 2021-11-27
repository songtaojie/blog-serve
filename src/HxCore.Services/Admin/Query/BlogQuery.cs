﻿using Hx.Sdk.Attributes;
using Hx.Sdk.Core;
using Hx.Sdk.Entity.Page;
using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model;
using HxCore.Model.Admin.Blog;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Services
{
    /// <summary>
    /// 博客查询
    /// </summary>
    public class BlogQuery : BaseQuery<T_Blog>, IBlogQuery
    {
        private readonly IWebManager _webManager;
        public BlogQuery(ISqlSugarRepository<T_Blog> repository, IWebManager webManager) :base(repository)
        {
            _webManager = webManager;
        }

        #region 客户端-查询
        [CacheData(AbsoluteExpiration = 5)]
        public async Task<SqlSugarPageModel<BlogQueryModel>> GetBlogsAsync(BlogQueryParam param)
        {
            var result = await this.Db.Queryable<T_Blog, T_Account>((b, u) => new JoinQueryInfos(JoinType.Inner, b.CreaterId == u.Id))
                .Where((b, u) => b.Publish == ConstKey.Yes && b.Deleted == ConstKey.No)
                .OrderBy((b, u) => b.PublishDate, OrderByType.Desc)
                .Select((b, u) => new BlogQueryModel
                {
                    Id = b.Id,
                    NickName = u.NickName,
                    UserName = u.AccountName,
                    Title = b.Title,
                    PureContent = b.PureContent,
                    ReadCount = b.ReadCount,
                    CmtCount = b.CmtCount,
                    PublishDate = b.PublishDate,
                    CoverImgUrl = b.CoverImgUrl,
                    AvatarUrl = u.AvatarUrl,
                    MarkDown = b.MarkDown
                })
                .ToPagedListAsync(param.PageIndex, param.PageSize);
            return result;
        }

        [CacheData(AbsoluteExpiration = 5)]
        public async Task<BlogDetailModel> FindById(string id)
        {
            var detail = await this.Db.Queryable<T_Blog, T_BlogExtend>((b, be) => new JoinQueryInfos(JoinType.Inner, b.Id == be.Id))
                .Where((b, be) => b.Id == id)
                .Select((b, be) => new BlogDetailModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Publish = b.Publish,
                    PublishDate = b.PublishDate,
                    Content = be.Content,
                    ReadCount = b.ReadCount,
                    CmtCount = b.CmtCount,
                    UserId = b.CreaterId,
                    NickName = b.Creater,
                    MarkDown = b.MarkDown
                })
                .FirstAsync();
            if (detail == null || detail.Publish == ConstKey.No) throw new UserFriendlyException("找不到您访问的页面", ErrorCodeEnum.DataNull);
            //获取上一个和下一个博客
            //await GetPreBlogInfo(blogModel);
            //await GetNextBlogInfo(blogModel);
            return detail;
        }
        private async Task GetPreBlogInfo(BlogDetailModel blogModel)
        {
            var preBlog = await this.Repository.Entities
                    .Where(b => b.CreaterId == blogModel.UserId && b.PublishDate < blogModel.PublishDate)
                    .OrderBy(b=>b.PublishDate,OrderByType.Desc)
                    .FirstAsync();
            if (preBlog != null)
            {
                blogModel.PreId = preBlog.Id.ToString();
                blogModel.PreTitle = preBlog.Title;
            }
        }

        private async Task GetNextBlogInfo(BlogDetailModel blogModel)
        {
            var nextBlog = await this.Repository.Entities
                .Where(b => b.CreaterId == blogModel.UserId && b.PublishDate > blogModel.PublishDate)
                .OrderBy(b => b.CreateTime)
                .FirstAsync();
            if (nextBlog != null)
            {
                blogModel.NextId = nextBlog.Id.ToString();
                blogModel.NextTitle = nextBlog.Title;
            }
        }
        #endregion

        #region 后台管理-查询
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        public async Task<SqlSugarPageModel<BlogManageQueryModel>> QueryBlogListAsync(BlogManageQueryParam param)
        {
            var query = this.Repository.Entities.Where(b => b.Deleted == ConstKey.No)
                    .OrderBy(b => b.PublishDate, OrderByType.Desc)
                    .OrderBy(b => b.CreateTime, OrderByType.Desc)
                    .Select(b => new BlogManageQueryModel
                    {
                        Id = b.Id,
                        Publish = b.Publish,
                        ReadCount = b.ReadCount,
                        Title = b.Title,
                        CmtCount = b.CmtCount,
                        CreateTime = b.CreateTime,
                        PublishDate = b.PublishDate,
                        Creater = b.Creater,
                        isMarkDown = b.MarkDown == ConstKey.Yes
                    });
            return await query.ToPagedListAsync(param.PageIndex,param.PageSize);
        }

        public async Task<BlogManageDetailModel> GetDetailAsync(string id)
        {
            var detailModel = await this.Db.Queryable<T_Blog, T_BlogExtend>((b, be) => new JoinQueryInfos(JoinType.Inner, b.Id == be.Id))
               .Where((b, be) => b.Publish == ConstKey.Yes && b.Deleted == ConstKey.No)
               .OrderBy((b, be) => b.PublishDate, OrderByType.Desc)
               .Select((b, be) => new BlogManageDetailModel
               {
                   Id = b.Id,
                   Title = b.Title,
                   BlogType = b.BlogType,
                   CanCmt = b.CanCmt,
                   Content = be.Content,
                   Publish = b.Publish,
                   BlogTags = b.BlogTags,
                   IsTop = b.IsTop,
                   CategoryId = b.CategoryId,
                   CoverImgUrl = b.CoverImgUrl,
                   MarkDown = b.MarkDown
               }).FirstAsync();

            if (detailModel == null) throw new UserFriendlyException("该文章不存在", ErrorCodeEnum.DataNull);
            if (!string.IsNullOrEmpty(detailModel.BlogTags))
            {
                var tagRepository = this.Repository.Change<T_TagInfo>();
                var blogtagIds = detailModel.BlogTags.Split(",").ToArray();
                detailModel.PersonTags = await tagRepository.Entities.Where(t => blogtagIds.Contains(t.Id))
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
            var tagRepository = this.Repository.Change<T_TagInfo>();
            return await tagRepository.Entities.Where(t => t.Deleted == ConstKey.No)
                .Select(t => new BlogManagePersonTag
                {
                    Id = t.Id.ToString(),
                    Name = t.Name
                }).ToListAsync();
        }
        #endregion
    }
}