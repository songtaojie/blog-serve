using HxCore.IServices;
using HxCore.Model.Client;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers.Client
{
    /// <summary>
    /// 网站首页控制器
    /// </summary>
    public class HomeController : BaseApiController
    {
        private readonly IBannerQuery _bannerQuery;
        private readonly INoticeQuery _noticeQuery;
        private readonly IFriendLinkQuery _friendLinkQuery;
        /// <summary>
        /// 首页控制器
        /// </summary>
        /// <param name="bannerQuery">首页横幅查询接口</param>
        /// <param name="noticeQuery">公告通知查询接口</param>
        /// <param name="friendLinkQuery">友情链接查询接口</param>
        public HomeController(IBannerQuery bannerQuery, INoticeQuery noticeQuery, IFriendLinkQuery friendLinkQuery)
        {
            _bannerQuery = bannerQuery;
            _noticeQuery = noticeQuery;
            _friendLinkQuery = friendLinkQuery;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/home/all")]
        public async Task<HomeAllDataModel> GetAllList()
        {
            var result = new HomeAllDataModel
            {
                Notices = await _noticeQuery.GetListAsync(5),
                Banners = await _bannerQuery.GetListAsync(5),
                FriendLinks = await _friendLinkQuery.GetListAsync()
            };
            return result;
        }


        /// <summary>
        /// 获取首页横幅
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/banners/{count?}")]
        public async Task<List<BannerModel>> GetBannerList(int count = 5)
        {
            return await _bannerQuery.GetListAsync(count);
        }

        /// <summary>
        /// 获取公告通知列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/notices/{count?}")]
        public async Task<List<NoticeModel>> GetNoticeList(int count = 5)
        {
            return await _noticeQuery.GetListAsync(count);
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/friendlinks")]
        public async Task<List<FriendLinkModel>> GetFriendLinkList()
        {
            return await _friendLinkQuery.GetListAsync();
        }
    }
}
