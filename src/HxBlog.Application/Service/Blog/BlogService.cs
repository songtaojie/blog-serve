
using HxBlog.Application.Service.Blog.Dto;
using HxBlog.Core;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace HxBlog.Application.Service.Blog
{
    /// <summary>
    /// 后台管理博客管理
    /// </summary>
    [ApiDescriptionSettings(ApplicationConst.Blog_GroupName, Order = 100)]
    public class BlogService : IDynamicApiController, ITransient
    {
        private readonly SqlSugarRepository<HtBlog> _htBlogRep;

        public BlogService(SqlSugarRepository<HtBlog> htBlogRep)
        {
            _htBlogRep = htBlogRep;
        }
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/blog/page")]
        [AllowAnonymous]
        public async Task<SqlSugarPagedList<PageBlogOutput>> GetBlogPage([FromQuery]PageBlogInput input)
        {
            var result = await _htBlogRep.AsQueryable()
                .Select(u => new PageBlogOutput
                {
                    IsAllowCmt = u.IsAllowCmt,
                    BlogType = u.BlogType,
                    CategoryId = u.CategoryId,
                    CmtNum = u.CmtNum,
                    CoverImgUrl = u.CoverImgUrl,
                    FavNum = u.FavNum,
                    IsMd = u.IsMd,
                    IsTop = u.IsTop,
                    OrderFactor = u.OrderFactor,
                    PublishDate = u.PublishDate,
                    PureContent = u.PureContent,
                    ReadNum = u.ReadNum,
                    SourceLink = u.SourceLink,
                    Status = u.Status,
                    Title = u.Title
                })
                .ToPagedListAsync(input.Page, input.PageSize);
            return result;
        }

        /// <summary>
        /// 添加博客
        /// </summary>
        /// <returns></returns>
        [HttpGet("/blog/add")]
        public async Task<bool> AddBlog(PageBlogInput input)
        {
            var blog = input.Adapt<HtBlog>();
            return await _htBlogRep.InsertAsync(blog);
        }
    }
}
