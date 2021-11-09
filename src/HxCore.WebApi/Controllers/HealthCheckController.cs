using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    public class HealthCheckController: BaseApiController
    {
        private readonly ILogger<HealthCheckController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 健康检查接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/HealthCheck")]
        //[SkipUnify]
        public IActionResult Get()
        {
            return Ok("ok");
        }

       

        /// <summary>
        /// elk日志测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> GetLog()
        {
            _logger.LogInformation("这是一条测试日志信息");
            _logger.LogWarning("这是一条测试日志信息Warn");
            _logger.LogError("这是一条测试日志信息Error");
            return new string[] { "value1", "value2" };
        }
    }
}
