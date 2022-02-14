using System;
using System.Threading.Tasks;
using Hx.Sdk.Attributes;
using Hx.Sdk.Core;
using Hx.Sdk.Entity.Page;
using HxCore.IServices;
using HxCore.IServices.SignalR;
using HxCore.Model.Admin.OperateLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace HxCore.WebApi.Controllers.Admin
{
    /// <summary>
    /// 主页
    /// </summary>
    [SkipOperateLog]
    [SkipRouteAuthorization]
    public class HomeController : Base.BaseAdminController
    {
        private readonly IOperateLogQuery _logQuery;
        private readonly IChatService _chatService;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="logQuery"></param>
        /// <param name="chatService"></param>
        public HomeController(IOperateLogQuery logQuery, IChatService chatService)
        {
            _logQuery = logQuery;
            _chatService = chatService;
        }

        /// <summary>
        ///  获取用户近三十天接口访问情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<OperateLogChars> GetLineDataAsync()
        {
            return await _logQuery.GetLineDataAsync();
        }

        /// <summary>
        /// 获取操作日志列表数据
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SqlSugarPageModel<OperateLogQueryModel>> GetLogsPageAsync(OperateLogQueryParam param)
        {
            return await _logQuery.QueryPageAsync(param);
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
