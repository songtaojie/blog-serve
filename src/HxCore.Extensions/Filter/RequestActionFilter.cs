using MediatR;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using HxCore.Extensions.Handlers;
using Microsoft.AspNetCore.Http;
using HxCore.Entity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using HxCore.Model.NotificationHandlers;

namespace Microsoft.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 请求日志拦截
    /// </summary>
    public class RequestActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            var httpRequest = httpContext.Request;

            var sw = new Stopwatch();
            sw.Start();
            var actionContext = await next();
            sw.Stop();

            
            var headers = httpRequest.Headers;
            IMediator mediator = httpContext.RequestServices.GetRequiredService<IMediator>();
            var agent = headers.ContainsKey("User-Agent") ? headers["User-Agent"].ToString() : string.Empty;
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            _ = mediator.Publish(new AddOperateLogModel
            {
                IPAddress = httpContext.GetRemoteIpAddressToIPv4(),
                Location = httpRequest.GetRequestUrlAddress(),
                Success = actionContext.Exception == null, // 判断是否请求成功（没有异常就是请求成功）
                Agent = agent,
                HttpMethod = httpRequest.Method,
                ControllerName = actionDescriptor.ControllerName,
                ActionName = actionDescriptor.ActionName,
                Url = httpRequest.Path,
                Param = JsonConvert.SerializeObject(context.ActionArguments.Count < 1 ? "" : context.ActionArguments),
                Result = actionContext.Result?.GetType() == typeof(JsonResult) ? JsonConvert.SerializeObject(actionContext.Result) : "",
                ElapsedTime = sw.ElapsedMilliseconds,
            });
        }
    }
}
