using Hx.Sdk.ConfigureOptions;
using HxCore.Extensions.Authentication;
using HxCore.Extensions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 权限启动服务注册
    /// </summary>
    public static class AuthorizationServiceCollectionExtensions
    {
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthoriationSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            
            // 1.使用基于角色的授权，仅仅在api上配置，第一步：[Authorize(Roles = "admin")]，第二步：配置统一认证服务，第三步：开启中间件
            services.AddAuthorization(c =>
            {
                c.AddPolicy(ConstInfo.SuperAdminPolicy, policy => policy.RequireRole(ConstInfo.SuperAdminPolicy, ConstInfo.ClientPolicy));
                c.AddPolicy(ConstInfo.AdminPolicy, policy => policy.RequireRole(ConstInfo.AdminPolicy));
                c.AddPolicy(ConstInfo.ClientPolicy, policy => policy.RequireRole(ConstInfo.ClientPolicy));
            });
            // 第二种方式、自定义复杂的策略授权
            //读取配置文件
            JwtSettings jwtSetting = AppSettings.GetConfig<JwtSettings>("JwtSettings");
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.SecretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            // 角色与接口的权限要求参数
            var permissionRequirement = new RouteAuthorizationRequirement(
                ClaimTypes.Role,//基于角色的授权
                jwtSetting.Issuer,//发行人
                jwtSetting.Audience,//听众
                signingCredentials,//签名凭据
                expiration: TimeSpan.FromSeconds(60 * 60)//接口的过期时间
                );
           
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstInfo.PermissionPolicy,
                         policy => policy.Requirements.Add(permissionRequirement));
            });

            // 注入权限处理器
            services.AddScoped<IAuthorizationHandler, RouteAuthorizationHandler>();
            services.AddSingleton(permissionRequirement);
        }
    }
}
