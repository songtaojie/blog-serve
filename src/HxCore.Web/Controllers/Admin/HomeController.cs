using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hx.Sdk.ConfigureOptions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HxCore.Web.Controllers.Admin
{
    /// <summary>
    /// 主页
    /// </summary>
    [Route("admin/[controller]/[action]")]
    [ApiController]
    public class HomeController : Base.BaseAdminController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            string result = $"【订单服务】{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}——" +
                $"{Request.HttpContext.Connection.LocalIpAddress}:{AppSettings.GetConfig("ConsulSetting:ServicePort")}";
            return result;
        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpPost,HttpGet]
        public string GetUserName()
        {
            var cookie = HttpContext.Request.Cookies["test"];
            return User.Identity.Name + cookie;
        }
    }
}
