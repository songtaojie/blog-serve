using HxCore.Extensions.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 授权中间件
    /// </summary>
    public static class AuthenticationAppBuilderExtensions
    {
        /// <summary>
        /// 使用自定义的jwt授权
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app)
        {
            app.UseMiddleware<JwtAuthenticationMiddleware>();
            return app;
        }

        ///// <summary>
        ///// 使用全局路由前缀设置
        ///// </summary>
        ///// <param name="opts"></param>
        ///// <param name="routeAttribute"></param>
        //public static void AddGlobalRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        //{
        //    // 添加我们自定义 实现IApplicationModelConvention的GlobalRouteConvention
        //    opts.Conventions.Insert(0, new GlobalRouteConvention(routeAttribute));
        //}
    }
}
