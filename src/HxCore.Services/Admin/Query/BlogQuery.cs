using Hx.Sdk.Attributes;
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
using HxCore.Model.Client;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly ISqlSugarRepository<T_TagInfo> _tagRepository;
        public BlogQuery(ISqlSugarRepository<T_Blog> repository, ISqlSugarRepository<T_TagInfo> tagRepository) : base(repository)
        {
            _tagRepository = tagRepository;
        }

        #region 博客-客户端-查询
        //[CacheData]
        public async Task<SqlSugarPageModel<BlogQueryModel>> GetBlogsAsync(BlogQueryParam param)
        {
            SqlSugarPageModel<BlogQueryModel> result;
            if (string.IsNullOrEmpty(param.TagId) || param.TagId == "0")
            {
                result = await this.Db.Queryable<T_Blog, T_Account>((b, u) => new JoinQueryInfos(JoinType.Inner, b.CreaterId == u.Id))
                   .Where((b, u) => b.Publish == ConstKey.Yes && b.Deleted == ConstKey.No)
                   .OrderBy((b, u) => b.IsTop, OrderByType.Desc)
                   .OrderBy((b, u) => b.PublishDate, OrderByType.Desc)
                   .Select((b, u) => new BlogQueryModel
                   {
                       Id = b.Id,
                       Publisher = b.Creater,
                       Title = b.Title,
                       BlogType = b.BlogType,
                       Top = b.IsTop,
                       PureContent = b.PureContent,
                       ReadCount = b.ReadCount,
                       CmtCount = b.CmtCount,
                       PublishDate = b.PublishDate,
                       CoverImgUrl = b.CoverImgUrl,
                       AvatarUrl = u.AvatarUrl
                   })
                   .ToPagedListAsync(param.PageIndex, param.PageSize);
            }
            else
            {
                result = await this.Db.Queryable<T_Blog, T_Account, T_BlogTag>((b, u, bt) =>
                     new JoinQueryInfos(JoinType.Inner, b.CreaterId == u.Id, JoinType.Left, b.Id == bt.BlogId))
                   .Where((b, u, bt) => b.Publish == ConstKey.Yes && b.Deleted == ConstKey.No && bt.TagId == param.TagId)
                   .OrderBy((b, u, bt) => b.IsTop, OrderByType.Desc)
                   .OrderBy((b, u, bt) => b.PublishDate, OrderByType.Desc)
                   .Select((b, u, bt) => new BlogQueryModel
                   {
                       Id = b.Id,
                       Publisher = b.Creater,
                       Title = b.Title,
                       BlogType = b.BlogType,
                       Top = b.IsTop,
                       PureContent = b.PureContent,
                       ReadCount = b.ReadCount,
                       CmtCount = b.CmtCount,
                       PublishDate = b.PublishDate,
                       CoverImgUrl = b.CoverImgUrl,
                       AvatarUrl = u.AvatarUrl
                   })
                   .ToPagedListAsync(param.PageIndex, param.PageSize);
            }
            //获取博客标签
            if (result.Items.Any())
            {
                var blogIds = result.Items.Select(b => b.Id).ToArray();
                var blogTags = await this.Db.Queryable<T_BlogTag, T_TagInfo>((bt, t) => new JoinQueryInfos(JoinType.Inner, bt.TagId == t.Id))
                    .Where((bt, t) => blogIds.Contains(bt.BlogId))
                    .Select((bt, t) => new
                    {
                        t.Id,
                        bt.BlogId,
                        t.Name,
                        t.BGColor,
                    })
                    .ToListAsync();
                foreach (var blog in result.Items)
                {
                    blog.PublishDate_V = GetDispayDate(blog.PublishDate);
                    blog.Tags = blogTags.Where(r => r.BlogId == blog.Id).Select(r => new TagModel { Id = r.Id, Name = r.Name, BGColor = r.BGColor });
                }
            }
            return result;
        }

        [CacheData(AbsoluteExpiration = 10)]
        public async Task<BlogDetailModel> Detail(string id)
        {
            var detail = await this.Db.Queryable<T_Blog, T_BlogExtend>((b, be) => new JoinQueryInfos(JoinType.Inner, b.Id == be.Id))
                .Where((b, be) => b.Id == id)
                .Select((b, be) => new BlogDetailModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublishDate = b.PublishDate,
                    Content = be.Content,
                    ReadCount = b.ReadCount,
                    CmtCount = b.CmtCount,
                    NickName = b.Creater,
                    IsMarkDown = SqlFunc.IF(b.MarkDown == ConstKey.Yes).Return(true).End(false),
                })
                .FirstAsync();
            if (detail == null || detail.Publish == ConstKey.No) throw new UserFriendlyException("找不到您访问的页面", ErrorCodeEnum.DataNull);
            return detail;
        }

        [CacheData]
        public async Task<HomeAllDataModel> GetHomeAllDataAsync()
        {
            IBannerQuery bannerQuery = this.Repository.ServiceProvider.GetRequiredService<IBannerQuery>();
            INoticeQuery noticeQuery = this.Repository.ServiceProvider.GetRequiredService<INoticeQuery>();
            IFriendLinkQuery friendLinkQuery = this.Repository.ServiceProvider.GetRequiredService<IFriendLinkQuery>();

            var result = new HomeAllDataModel
            {
                Notices = await noticeQuery.GetListAsync(5),
                Banners = await bannerQuery.GetListAsync(5),
                FriendLinks = await friendLinkQuery.GetListAsync(),
                Tags = await GetTagListAsync(),
                Hots = await GetHotBlogAsync(3)
            };
            return result;
        }
        /// <summary>
        /// 获取博客标签列表-客户端使用
        /// </summary>
        /// <returns></returns>
        public async Task<List<HotBlogModel>> GetHotBlogAsync(int count)
        {
            return await this.Repository.Entities.Where(b => b.Disabled == ConstKey.No && b.Deleted == ConstKey.No)
                  .OrderBy(b => b.OrderFactor, OrderByType.Desc)
                  .Take(count)
                  .Select(b => new HotBlogModel
                  {
                      Id = b.Id,
                      Title = b.Title,
                      ReadCount = b.ReadCount,
                      CmtCount = b.CmtCount,
                      CoverImgUrl = b.CoverImgUrl,
                  })
                  .ToListAsync();
        }

        #endregion

        #region 标签/栏目-客户端

        /// <summary>
        /// 获取博客标签列表-客户端使用
        /// </summary>
        /// <returns></returns>
        public async Task<List<TagModel>> GetTagListAsync()
        {
            return await _tagRepository.Entities.Where(t => t.Deleted == ConstKey.No)
                .Select(t => new TagModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    BGColor = t.BGColor
                }).ToListAsync();
        }

        #endregion

        #region 博客-后台管理-查询
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        public async Task<SqlSugarPageModel<BlogManageQueryModel>> QueryBlogListAsync(BlogManageQueryParam param)
        {
            var query = this.Db.Queryable<T_Blog>().Where(b => b.Deleted == ConstKey.No)
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
                        IsTop = b.IsTop,
                        PublishDate = b.PublishDate,
                        Creater = b.Creater,
                        isMarkDown = b.MarkDown == ConstKey.Yes
                    });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }

        public async Task<BlogManageDetailModel> GetDetailAsync(string id)
        {
            var detailModel = await this.Db.Queryable<T_Blog, T_BlogExtend>((b, be) => new JoinQueryInfos(JoinType.Inner, b.Id == be.Id))
               .Where((b, be) => b.Id == id && b.Publish == ConstKey.Yes && b.Deleted == ConstKey.No)
               .OrderBy((b, be) => b.PublishDate, OrderByType.Desc)
               .Select((b, be) => new BlogManageDetailModel
               {
                   Id = b.Id,
                   Title = b.Title,
                   BlogType = b.BlogType,
                   CanCmt = b.CanCmt,
                   Content = be.Content,
                   Publish = b.Publish,
                   IsTop = b.IsTop,
                   CategoryId = b.CategoryId,
                   CoverImgUrl = b.CoverImgUrl,
                   MarkDown = b.MarkDown
               }).FirstAsync();

            if (detailModel == null) throw new UserFriendlyException("该文章不存在", ErrorCodeEnum.DataNull);
            detailModel.BlogTags = await this.Db.Queryable<T_BlogTag>().Where(bt => bt.BlogId == detailModel.Id)
                .Select(bt => bt.TagId)
                .ToListAsync();
            return detailModel;
        }

        /// <summary>
        /// 获取博客标签列表-后台使用
        /// </summary>
        /// <returns></returns>
        public async Task<List<TagManageModel>> GetTagManageListAsync()
        {
            return await _tagRepository.Entities.Where(t => t.Deleted == ConstKey.No)
                .Select(t => new TagManageModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToListAsync();
        }
        #endregion

        #region 标签/栏目-后台管理

        public async Task<TagManageModel> GetTagDetailAsync(string tagId)
        {
            var detail = await _tagRepository.Entities.Where(r => r.Id == tagId)
                .Select(r => new TagManageModel
                {
                    Id = r.Id,
                    BGColor = r.BGColor,
                    Name = r.Name,
                    OrderSort = r.OrderSort,
                    IsEnabled = r.Disabled == ConstKey.No
                })
                .FirstAsync();
            if (detail == null) throw new UserFriendlyException("该标签不存在", ErrorCodeEnum.DataNull);
            return detail;
        }

        public async Task<CategoryManageModel> GetCategoryDetailAsync(string categoryId)
        {
            var categoryRepository = this.Repository.Change<T_Category>();
            var detail = await categoryRepository.Entities.Where(r => r.Id == categoryId)
                .Select(r => new CategoryManageModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    OrderSort = r.OrderSort,
                    Description = r.Description,
                    IsEnabled = r.Disabled == ConstKey.No
                })
                .FirstAsync();
            if (detail == null) throw new UserFriendlyException("该栏目不存在", ErrorCodeEnum.DataNull);
            return detail;
        }

        public async Task<SqlSugarPageModel<TagManageModel>> QueryTagPageAsync(BasePageParam param)
        {
            var list = await _tagRepository.Entities
                .Where(r => r.Deleted == ConstKey.No)
                .OrderBy(r => r.OrderSort, OrderByType.Desc)
                .OrderBy(r => r.CreateTime, OrderByType.Desc)
                .Select(r => new TagManageModel
                {
                    Id = r.Id,
                    BGColor = r.BGColor,
                    Name = r.Name,
                    OrderSort = r.OrderSort,
                    IsEnabled = r.Disabled == ConstKey.No
                })
                .ToPagedListAsync(param.PageIndex, param.PageSize);
            return list;
        }

        public async Task<SqlSugarPageModel<CategoryManageModel>> QueryCategoryPageAsync(BasePageParam param)
        {
            var categoryRepository = this.Repository.Change<T_Category>();
            var list = await categoryRepository.Entities
                .Where(r => r.Deleted == ConstKey.No)
                .OrderBy(r => r.OrderSort, OrderByType.Desc)
                .OrderBy(r => r.CreateTime, OrderByType.Desc)
                .Select(r => new CategoryManageModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    OrderSort = r.OrderSort,
                    Description = r.Description,
                    IsEnabled = r.Disabled == ConstKey.No
                })
                .ToPagedListAsync(param.PageIndex, param.PageSize);
            return list;
        }


        #endregion

        #region  私有函数
        #region 日期处理函数
        private string GetDispayDate(DateTime? date, bool showTime = false)
        {
            if (!date.HasValue) return "";
            return GetDispayDate(date.Value, showTime);
        }
        private string GetDispayDate(DateTime date, bool showTime = false)
        {
            TimeSpan ts = DateTime.Now.Subtract(date);
            if (ts.TotalMinutes < 10) return "刚刚";
            if (ts.TotalMinutes < 60) return string.Format("{0} 分钟前", (int)Math.Floor(ts.TotalMinutes));
            if (ts.TotalHours <= 24) return string.Format("{0} 小时前", (int)Math.Floor(ts.TotalHours));
            if (ts.TotalDays <= 7) return string.Format("{0} 天前", (int)Math.Floor(ts.TotalDays));
            if (date.Year == DateTime.Now.Year)
            {
                string timeFormat = showTime ? "MM-dd HH:ss" : "MM-dd";
                return date.ToString(timeFormat);
            }
            string format = showTime ? "yyyy-MM-dd HH:ss" : "yyyy-MM-dd";
            return date.ToString(format); ;
        }
        #endregion
        #endregion
    }
}
