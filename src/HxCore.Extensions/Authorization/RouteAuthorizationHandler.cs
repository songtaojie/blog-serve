using Hx.Sdk.Core;
using HxCore.IServices.Admin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hx.Sdk.Extensions;
using Hx.Sdk.Attributes;

namespace Microsoft.AspNetCore.Authorization
{
    //Infrastructure.OperationAuthorizationRequirement
    //Infrastructure.RolesAuthorizationRequirement
    /// <summary>
    /// 权限授权处理器
    /// </summary>
    public class RouteAuthorizationHandler : AuthorizationHandler<RouteAuthorizationRequirement>
    {
        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }
        private readonly IUserContext _userContext;
        private readonly IPermissionService _service;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="roleService"></param>
        /// <param name="accessor"></param>
        public RouteAuthorizationHandler(IAuthenticationSchemeProvider schemes, IUserContext userContext, IPermissionService service)
        {
            Schemes = schemes;
            _userContext = userContext;
            _service = service;
        }

        // 重写异步处理程序
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RouteAuthorizationRequirement requirement)
        {
            var httpContext = _userContext.HttpContext;
            var endpoint = httpContext.GetEndpoint();
            // 判断action上是否有跳过授权策略的
            var skipAuthorization = endpoint.Metadata.GetMetadata<SkipRouteAuthorizationAttribute>();
            if (skipAuthorization != null)
            {
                context.Succeed(requirement);
                return;
            }
            //判断控制器上是否标记有SkipRouteAuthorizationAttribute
            var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            var hasSkipAuthorization = actionDescriptor.ControllerTypeInfo.HasAttribute<SkipRouteAuthorizationAttribute>();
            if (hasSkipAuthorization)
            {
                context.Succeed(requirement);
                return;
            }
            //处理授权策略，判断该用户能否访问该接口
            var questUrl = httpContext.Request.Path.Value.ToLower();
            //var userId = _userContext.UserId;
            //var cacheKey = string.Format(CacheKeyConfig.AuthRouterKey, userId);
            var cacheData = await _service.GetUserPermissionAsync();
            if (cacheData == null)
            {
                context.Fail();
                return;
            }
            if (cacheData.IsSuperAdmin)
            {
                context.Succeed(requirement);
            }
            else
            {
                // 整体结构类似认证中间件UseAuthentication的逻辑，具体查看开源地址
                // https://github.com/dotnet/aspnetcore/blob/master/src/Security/Authentication/Core/src/AuthenticationMiddleware.cs
                httpContext.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
                {
                    OriginalPath = httpContext.Request.Path,
                    OriginalPathBase = httpContext.Request.PathBase
                });
                // Give any IAuthenticationRequestHandler schemes a chance to handle the request
                // 主要作用是: 判断当前是否需要进行远程验证，如果是就进行远程验证
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
                {
                    if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                    {
                        context.Fail();
                        return;
                    }
                }

                //判断请求是否拥有凭据，即有没有登录
                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    //result?.Principal不为空即登录成功
                    if (result?.Principal != null)
                    {
                        //if (!isTestCurrent)
                        httpContext.User = result.Principal;
                        cacheData.Modules.ForEach(m =>
                        {
                            if (!string.IsNullOrEmpty(m.RouteUrl) && m.RouteUrl.Contains("{") && m.RouteUrl.Contains("}"))
                            {
                                var regex = new Regex("\\{[a-z]+\\}");
                                var matchs = regex.Matches(m.RouteUrl);
                                foreach (var route in matchs)
                                {
                                    var routeKey = route.ToString().Replace("{", "").Replace("}", "");
                                    var routeValue = httpContext.Request.RouteValues[routeKey].ToString();
                                    m.RouteUrl = m.RouteUrl.Replace("{" + routeKey + "}", routeValue);
                                }
                            }
                        });
                        // 获取当前用户的角色信息
                        var isMatch = cacheData.Modules.Any(m => FixRoute(questUrl).Equals(FixRoute(m.RouteUrl), StringComparison.OrdinalIgnoreCase));
                        if (isMatch)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 修复路由
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        private string FixRoute(string route)
        {
            if (string.IsNullOrEmpty(route)) return string.Empty;
            if (route.StartsWith("/")) return route[1..];
            return route;
        }
    }
}
