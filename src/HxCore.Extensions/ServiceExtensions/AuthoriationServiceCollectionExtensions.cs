using Hx.Sdk.ConfigureOptions;
using HxCore.Extensions.Common;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 权限启动服务注册
    /// </summary>
    public static class AuthoriationServiceCollectionExtensions
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

            if (App.Settings.UseIdentityServer4 == true)
            {
                services.AddIds4Authentication();
            }
            else
            {
                services.AddJwtAuthentication();
            }
        }
    }
}
