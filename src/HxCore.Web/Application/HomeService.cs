using Hx.Sdk.Core;
using Hx.Sdk.UnifyResult;
using HxCore.Model.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.Web.Application
{
    public class HomeService
    {
        private readonly IHxHttpClient _client;
        private readonly string blogServiceApi = AppSettings.GetConfig("BlogServiceApi");
        public HomeService(IHxHttpClient client)
        {
            _client = client;
        }


        /// <summary>
        /// 获取首页横幅数据
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<List<BannerModel>> GetBannerList()
        {
            string url = string.Format("{0}/api/banners", blogServiceApi);
            var result = await _client.GetAsync<RESTfulResult<List<BannerModel>>>(url);
            if (!result.Succeeded) throw new Exception(result.Message);
            return result.Data;
        }

        /// <summary>
        /// 获取公告通知列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<NoticeModel>> GetNoticeList()
        {
            string url = string.Format("{0}/api/notices", blogServiceApi);
            var result = await _client.GetAsync<RESTfulResult<List<NoticeModel>>>(url);
            if (!result.Succeeded) throw new Exception(result.Message);
            return result.Data;
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <returns></returns>
        public async Task<List<FriendLinkModel>> GetFriendLinkList()
        {
            string url = string.Format("{0}/api/friendlinks", blogServiceApi);
            var result = await _client.GetAsync<RESTfulResult<List<FriendLinkModel>>>(url);
            if (!result.Succeeded) throw new Exception(result.Message);
            return result.Data;
        }
    }
}
