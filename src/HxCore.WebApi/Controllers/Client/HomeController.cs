using HxCore.IServices;
using HxCore.Model.Client;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers.Client
{
    /// <summary>
    /// 网站首页控制器
    /// </summary>
    public class HomeController : BaseApiController
    {
        private readonly ITimeLineQuery _timeLineQuery;
        private readonly IBlogQuery _blogQuery;
        /// <summary>
        /// 首页控制器
        /// </summary>
        /// <param name="timeLineQuery">时间戳查询接口</param>
        /// <param name="blogQuery">博客查询接口</param>
        public HomeController(ITimeLineQuery timeLineQuery, IBlogQuery blogQuery)
        {
            _timeLineQuery = timeLineQuery;
            _blogQuery = blogQuery;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/home/all")]
        public async Task<HomeAllDataModel> GetAllList()
        {
            return await _blogQuery.GetHomeAllDataAsync();
        }


        /// <summary>
        /// 获取首页横幅
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/timelines/{page}")]
        public async Task<SqlSugarPageModel<TimeLineModel>> GetTimeLineList(int page)
        {
            return await _timeLineQuery.GetPageAsync(page,20);
        }
       
    }
}
