using MediatR;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using HxCore.Model.NotificationHandlers;
using Hx.Sdk.Attributes;
using Hx.Sdk.Extensions;

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

            //判断控制器上是否标记有跳过日志记录
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var hasSkipAuthorization = actionDescriptor.ControllerTypeInfo.HasAttribute<SkipOperateLogAttribute>();
            if (hasSkipAuthorization) return;

            // 判断action上是否有跳过日志记录
            var endpoint = httpContext.GetEndpoint();
            var skipOperateLog = endpoint.Metadata.GetMetadata<SkipOperateLogAttribute>();
            if (skipOperateLog != null) return;
           
            var model = new AddOperateLogModel
            {
                IPAddress = httpContext.GetLocalIpAddressToIPv4(),
                Location = httpRequest.GetRequestUrlAddress(),
                Success = actionContext.Exception == null, // 判断是否请求成功（没有异常就是请求成功）
                Agent = agent,
                HttpMethod = httpRequest.Method,
                ControllerName = actionDescriptor.ControllerName,
                ActionName = actionDescriptor.ActionName,
                Url = httpRequest.Path,
                Param = JsonConvert.SerializeObject(context.ActionArguments.Count < 1 ? "" : context.ActionArguments),
                ElapsedTime = sw.ElapsedMilliseconds,
            };
            model.Result = actionContext.Result switch
            {
                ObjectResult result => JsonConvert.SerializeObject(result.Value),
                JsonResult result => JsonConvert.SerializeObject(result.Value),
                ContentResult result => result.Content,
                _ => actionContext.Exception == null? string.Empty: actionContext.Exception.Message
            };
            _ = mediator.Publish(model);
        }
    }
}
