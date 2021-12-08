using Hx.Sdk.Core;
using Hx.Sdk.Entity.Page;
using Hx.Sdk.UnifyResult;
using HxCore.Model;
using HxCore.Model.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Web.Application
{
    public class BlogService
    {
        private readonly IHxHttpClient _client;
        private readonly string blogServiceApi = AppSettings.GetConfig("BlogServiceApi");
        public BlogService(IHxHttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<PageModel<BlogQueryModel>> GetArticleList(int pageIndex)
        {
            string url = string.Format("{0}/api/articles", blogServiceApi);
            var param = new BlogQueryParam
            {
                PageIndex = pageIndex,
                PageSize = 10
            };
            var result = await _client.PostAsync<RESTfulResult<PageModel<BlogQueryModel>>>(url, param);
            if (!result.Succeeded) throw new Exception(result.Message);
            return result.Data;
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        public async Task<BlogDetailModel> GetArticle(string id)
        {
            string url = string.Format("{0}/api/article/{1}", blogServiceApi, id);
            var result = await _client.GetAsync<RESTfulResult<BlogDetailModel>>(url);
            if (!result.Succeeded) throw new Exception(result.Message);
            return result.Data;
        }

    }
}
