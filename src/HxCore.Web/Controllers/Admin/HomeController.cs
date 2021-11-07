using System;
using System.Threading.Tasks;
using Hx.Sdk.Attributes;
using Hx.Sdk.Core;
using Hx.Sdk.Entity.Page;
using HxCore.IServices.Admin;
using HxCore.IServices.SignalR;
using HxCore.Model.Admin.OperateLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HxCore.Web.Controllers.Admin
{
    /// <summary>
    /// 主页
    /// </summary>
    [SkipOperateLog]
    [SkipRouteAuthorization]
    public class HomeController : Base.BaseAdminController
    {
        private readonly IOperateLogService _logService;
        private readonly IChatService _chatService;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="logService"></param>
        /// <param name="chatService"></param>
        public HomeController(IOperateLogService logService, IChatService chatService)
        {
            _logService = logService;
            _chatService = chatService;
        }


        /// <summary>
        ///  获取用户近三十天接口访问情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<OperateLogChars> GetLineDataAsync()
        {
            return await _logService.GetLineDataAsync();
        }

        /// <summary>
        /// 获取操作日志列表数据
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<OperateLogQueryModel>> GetLogsPageAsync(OperateLogQueryParam param)
        {
            return await _logService.QueryPageAsync(param);
        }

        /// <summary>
        /// SignalR发送消息接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public string SendMessage()
        {
            _chatService.SendMessage("1111111111", "hahha");
            return "success";
        }

        /// <summary>
        /// 测试接口
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
        /// 获取cookie
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
