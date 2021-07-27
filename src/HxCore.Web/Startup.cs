using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HxCore.Extensions.Filter;
using Microsoft.Extensions.Hosting;
using System;
using Hx.Sdk.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Hx.Sdk.ConfigureOptions;

namespace HxCore.Web
{
    /// <summary>
    /// 启动
    /// </summary>
    public class Startup
    {
        
        private IHostEnvironment Environment { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_env">环境</param>
        public Startup(IHostEnvironment _env)
        {
            Environment = _env;
        }
        /// <summary>
        /// 服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine();
            services.AddWebManager();
            #region 缓存
            services.AddNativeMemoryCache();
            services.AddRedisCache();
            #endregion

            #region Authorize 权限三步走
            services.AddAuthoriationSetup();
            services.AddAuthenticationSetup();
            //services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            #endregion

            #region AutoMapper
            services.AddAutoMapperSetup();
            #endregion

            #region 数据库链接，上下文
            ConsoleHelper.WriteInfoLine(AppSettings.GetConfig("ConnectionStrings:MySqlConnectionString"));
            services.AddDatabaseAccessor(service =>
            {
                service.AddDbPool<Entity.Context.DefaultContext>();
                service.AddDbPool<Entity.Context.IdsDbContext, Entity.Context.IdsDbContextLocator>();
            }, "HxCore.Entity");
            #endregion

            #region MVC，路由配置
            services.AddControllers()
                //.AddUnifyResult()
                .AddJsonOptions(json => {
                    json.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    json.JsonSerializerOptions.Converters.Add(new DateTimeNullConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            #endregion

            #region 跨域CORS
            services.AddCorsAccessor();
            #endregion

            #region Options,Configure
            services.AddCustomOptions();
            #endregion

            //#region 原生的依赖注入
            //使用时记得把ConfigureContainer中的Autofac注入去掉,
            //services.AddPrimitiveDI(Environment);
            //#endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lifetime"></param>
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime  lifetime)
        {
            if (Environment.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            app.UseRouting();//路由中间件
            app.UseCorsAccessor();
            //app.UseJwtTokenAuth();
            //使用官方的认证，ConfigureServices中的AddAuthentication和AddJwtBearer缺一不可
            // 先开启认证
            app.UseAuthentication();
            // 然后是授权中间件
            app.UseAuthorization();
            app.UseCookiePolicy();
            //app.UseStatusCodePages();
            // 短路中间件，配置Controller路由
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
            //app.UseConsulService(lifetime);
        }
    }
}
