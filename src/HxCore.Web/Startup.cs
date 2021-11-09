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
namespace HxCore.Web
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
            services.AddDatabaseAccessor(service =>
            {
                service.AddDbPool<Entity.Context.DefaultContext>();
                service.AddDbPool<Entity.Context.IdsDbContext, Entity.Context.IdsDbContextLocator>();
            });
            #endregion

            #region MVC，路由配置
            services.AddControllers(options =>
                {
                    options.Filters.Add(new RequestActionFilter());
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                })
                //.AddUnifyResult()
                //.AddJsonOptions(json => {
                //    json.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                //    json.JsonSerializerOptions.Converters.Add(new DateTimeNullConverter());
                //})
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            #endregion

            #region MediatR
            //services.AddMediatR(typeof(Startup));
            services.AddMediatR(App.Assemblies.ToArray());
            #endregion 

            #region SignalR
            services.AddSignalR();
                //.AddMessagePackProtocol().AddStackExchangeRedis(Configuration["DbConfig:Redis:ConnectionString"]);
            #endregion 

            #region 跨域CORS
            services.AddCorsAccessor();
            #endregion

            #region Options,Configure
            services.AddCustomOptions();
            #endregion

            #region CAP
            services.AddCapRabbitMQ();
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
            //app.UseCap();
            // 短路中间件，配置Controller路由
            app.UseConsulService(lifetime);
            app.UseDatabaseAccessor();
            app.UseCapRabbitMQ();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
                //这里要说下，为啥地址要写 /api/xxx 
                //因为我前后端分离了，而且使用的是代理模式，所以如果你不用/api/xxx的这个规则的话，会出现跨域问题，
                //毕竟这个不是我的controller的路由，而且自己定义的路由
                endpoints.MapHub<ChatHub>("/api/chathub");
            });
        }
    }
}
