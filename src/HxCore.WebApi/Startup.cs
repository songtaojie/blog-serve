using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hx.Sdk.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using MediatR;
using System.Linq;
using HxCore.Services.SignalR;
using System;

namespace HxCore.WebApi
{
    /// <summary>
    /// 启动
    /// </summary>
    public class Startup
    {

        private IHostEnvironment Environment { get; }

        private IConfiguration  Configuration { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_env">环境</param>
        /// <param name="configuration">配置</param>
        public Startup(IHostEnvironment _env, IConfiguration configuration)
        {
            Environment = _env;
            Configuration = configuration;
        }
        /// <summary>
        /// 服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Options,Configure
            services.AddCustomOptions();
            #endregion

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
            ConsoleHelper.WriteInfoLine(Configuration.GetConnectionString("MySqlConnectionString"));
            services.AddDatabaseAccessor();
            var sqlSugurSetting = new SqlSugar.ConnectionConfig
            {
                ConnectionString = Configuration.GetConnectionString("MySqlConnectionString"),
                DbType = SqlSugar.DbType.MySql,
                IsAutoCloseConnection = true,
            };
            //开启日志记录
            if (Configuration.GetValue<bool>("DbSettings:EnabledSqlLog", false))
            {
                sqlSugurSetting.AopEvents = new SqlSugar.AopEvents
                {
                    //多库状态下每个库必须单独绑定打印事件，否则只会打印第一个库的sql日志
                    OnLogExecuting = (sql, pars) =>
                    {
                        Console.WriteLine(SqlSugar.SqlProfiler.ParameterFormat(sql, pars));
                        Console.WriteLine();
                    }
                };
            }
            services.AddSqlSugar(sqlSugurSetting);
            #endregion

            #region MVC，路由配置
            services.AddControllers()
                .AddMvcFilter<RequestActionFilter>()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                })
                //.AddJsonOptions(json => {
                //    json.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                //    json.JsonSerializerOptions.Converters.Add(new DateTimeNullConverter());
                //})
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            #endregion

            #region MediatR
            services.AddMediatR(App.Assemblies.ToArray());
            #endregion 

            #region SignalR
            services.AddSignalR();
                //.AddMessagePackProtocol().AddStackExchangeRedis(Configuration["DbConfig:Redis:ConnectionString"]);
            #endregion 

            #region 跨域CORS
            services.AddCorsAccessor();
            #endregion

            #region CAP
            if(Configuration.GetValue<bool>("CapRabbitMQSettings:Enabled",false)) services.AddCapRabbitMQ();
            #endregion

            #region ElasticSearch
            services.AddElasticSearch();
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
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
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

            //app.UseHttpsRedirection();

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
            //app.UseCap();
            // 短路中间件，配置Controller路由
            //app.UseConsulService(lifetime);
            app.UseDatabaseAccessor();
            if(Configuration.GetValue("CapRabbitMQSettings:Enabled",false)) app.UseCapRabbitMQ();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
