using HxCore.IServices;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HxCore.WebApi.Controllers.Client
{
    /// <summary>
    /// 首页横幅控制器
    /// </summary>
    public class BannerController : BaseApiController
    {
        private readonly IBannerQuery _query;

        /// <summary>
        /// 控制器
        /// </summary>
        /// <param name="query">查询接口</param>
        public BannerController(IBannerQuery query)
        { 
            _query = query;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
