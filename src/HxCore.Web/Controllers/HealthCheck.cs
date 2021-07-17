using HxCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Web.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    public class HealthCheck: BaseApiController
    {
        /// <summary>
        /// 健康检查接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/[controller]")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
