using Hx.Sdk.Cache;
using Hx.Sdk.ConfigureOptions;
using Hx.Sdk.Core;
using HxCore.Entity.Permission;
using HxCore.IServices.Admin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Authorization
{
    //Infrastructure.OperationAuthorizationRequirement
    //Infrastructure.RolesAuthorizationRequirement
    /// <summary>
    /// 权限授权处理器
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }
        private readonly IRoleService _roleService;
        private readonly IUserContext _userContext;
        private readonly IRedisCache _redisCache;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="roleService"></param>
        /// <param name="accessor"></param>
        public PermissionHandler(IAuthenticationSchemeProvider schemes, IRoleService roleService, IUserContext userContext,IRedisCache redisCache)
        {
            _userContext = userContext;
            Schemes = schemes;
            _roleService = roleService;
            _redisCache = redisCache;
        }

        // 重写异步处理程序
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            if (_userContext.IsSuperAdmin) 
            { 
                
            }

            var httpContext = _userContext.HttpContext;
            var isUserIds4 = App.Settings.UseIdentityServer4 ?? false;
            // 获取系统中所有的角色和菜单的关系集合
            if (!requirement.Permissions.Any())
            {
                var data = await _roleService.GetAllRoleMenusAsync();
                requirement.Permissions = data.Select(r => new PermissionItem
                {
                    Role = r.RoleId,
                    Menu = r.MenuId
                }).ToList();
            }
            if (!_userContext.IsSuperAdmin && httpContext != null)
            {
                var c = context.Resource as HttpContext;
                var endpoint = httpContext.GetEndpoint();
                var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                var questUrl = httpContext.Request.Path.Value.ToLower();
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
                        // 获取当前用户的角色信息
                        var currentUserRoles = _userContext.GetClaimValueByType("roleId");
                        var isMatchRole = false;
                        var permisssionRoles = requirement.Permissions.Where(w => currentUserRoles.Contains(w.Role));


                        var permissionAttr = endpoint.Metadata.GetMetadata<PermissionAttribute>();

                        ////验证权限
                        //if (currentUserRoles.Count <= 0 || !isMatchRole)
                        //{
                        //    context.Fail();
                        //    return;
                        //}
                       
                        //if (isExp)
                        //{
                        //    context.Succeed(requirement);
                        //}
                        //else
                        //{
                        //    context.Fail();
                        //    return;
                        //}
                        return;
                    }
                }
                //判断没有登录时，是否访问登录的url,并且是Post请求，并且是form表单提交类型，否则为失败
                if (!(questUrl.Equals(requirement.LoginPath.ToLower(), StringComparison.Ordinal) && (!httpContext.Request.Method.Equals("POST") || !httpContext.Request.HasFormContentType)))
                {
                    context.Fail();
                    return;
                }
            }
            //context.Succeed(requirement);
        }
    }
}
