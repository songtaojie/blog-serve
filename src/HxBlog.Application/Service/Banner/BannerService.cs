using HxBlog.Application.Service.Banner.Dto;
using HxBlog.Core;
using Mapster;

namespace HxBlog.Application.Service.Banner
{
    /// <summary>
    /// 首页横幅控制器
    /// </summary>
    [ApiDescriptionSettings(ApplicationConst.Blog_GroupName, Order = 100)]
    public class BannerService : IDynamicApiController, ITransient
    {
        private readonly SqlSugarRepository<HtBanner> _htBannerRep;

        public BannerService(SqlSugarRepository<HtBanner> htBannerRep)
        {
            _htBannerRep = htBannerRep;
        }

        #region 查询
        /// <summary>
        /// 获取首页横幅分页列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Banner/page")]
        public async Task<SqlSugarPagedList<PageBannerOutput>> GetPageAsync([FromQuery]PageBannerInput input)
        {
            return await _htBannerRep.AsQueryable()
                .OrderBy(u => u.CreateTime, OrderByType.Desc)
                .Select(u => new PageBannerOutput
                {
                    Id = u.Id,
                    ImgUrl= u.ImgUrl,
                    Url = u.Url,
                    Target= u.Target,
                    Title= u.Title,
                    Sort = u.Sort
                })
                .ToPagedListAsync(input.Page, input.PageSize);
        }

        /// <summary>
        /// 获取首页横幅详情数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Banner/detail/{id}")]
        public async Task<PageBannerOutput> GetAsync(long id)
        {
            var result = await _htBannerRep.GetFirstAsync(u=>u.Id == id);
            return result.Adapt<PageBannerOutput>();
        }
        #endregion

        #region 操作
       
        #endregion
    }

}
