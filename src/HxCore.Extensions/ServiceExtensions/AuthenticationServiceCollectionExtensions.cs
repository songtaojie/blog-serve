using Hx.Sdk.Cache;
using Hx.Sdk.Common.Extensions;
using Hx.Sdk.Core;
using HxCore.Entity;
using HxCore.Extensions.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationServiceCollectionExtensions
    {


        public static void AddAuthenticationSetup(this IServiceCollection services)
        {
            if (App.Settings.UseIdentityServer4 == true)
            {
                services.AddIds4Authentication();
            }
            else
            {
                services.AddJwtAuthentication();
            }
        }

        /// <summary>
        /// 添加IdentityServer4认证 
        /// </summary>
        /// <param name="services"></param>
        public static void AddIds4Authentication(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            // 添加Identityserver4认证
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = nameof(MyJwtBearerHandler);
                o.DefaultForbidScheme = nameof(MyJwtBearerHandler);
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
            {
                var authority = AppSettings.GetConfig(new string[] { "Ids4Settings", "Authority" });
                var audience = AppSettings.GetConfig(new string[] { "Ids4Settings", "Audience" });
                var requireHttps = AppSettings.GetConfig(new string[] { "Ids4Settings", "RequireHttps" }).ToBool();
                options.Audience = audience;
                options.Authority = authority;
                options.RequireHttpsMetadata = requireHttps??false;
                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    ValidateAudience = false
                //};
                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Headers[HeaderNames.Authorization];
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var token = context.Request.Headers[HeaderNames.Authorization];
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var token = context.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "");
                        var jwtToken = (new JwtSecurityTokenHandler()).ReadJwtToken(token);

                        if (jwtToken.Audiences.FirstOrDefault() != audience)
                        {
                            context.Response.Headers.Add("Token-Error-Audience", "true");
                        }

                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
                //options.Audience = AppSettings.Get(new string[] { "Startup", "IdentityServer4", "Audience" });
            })
            .AddScheme<AuthenticationSchemeOptions, MyJwtBearerHandler>(nameof(MyJwtBearerHandler), o => { });
        }

        /// <summary>
        /// 添加jwt的授权认证方案
        /// </summary>
        /// <param name="services"></param>
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //配置认证服务
            JwtSettings jwtSetting = AppSettings.GetConfig<JwtSettings>("JwtSettings");
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.SecretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //令牌验证参数
            TokenValidationParameters tokenParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSetting.Issuer,
                ValidAudience = jwtSetting.Audience,
                ValidateLifetime = true,//是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 
                ClockSkew = TimeSpan.FromSeconds(1),
                RequireExpirationTime = true,
            };
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.LoginPath = "/Account/Login";
            //    options.LogoutPath = "/Account/Logout";
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Cookie.HttpOnly = false;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            //    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
            //});
            // 开启Bearer认证
            services.AddAuthentication(c =>
            {
                //c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = nameof(MyJwtBearerHandler);
                c.DefaultForbidScheme = nameof(MyJwtBearerHandler);
            })
            // 添加JwtBearer服务
            .AddJwtBearer(c =>
            {
                c.TokenValidationParameters = tokenParams;
                c.Events = new JwtBearerEvents()
                {
                    //OnChallenge = context =>
                    //{
                    //    context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                    //    return Task.CompletedTask;
                    //},
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/api/chathub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                    //OnTokenValidated = context => TokenValidated(context),
                    OnAuthenticationFailed = context => AuthenticationFailed(context)
                };
            })
            .AddScheme<AuthenticationSchemeOptions, MyJwtBearerHandler>(nameof(MyJwtBearerHandler), o => { });
        }

        public static async Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            JwtSettings jwtSetting = AppSettings.GetConfig<JwtSettings>("JwtSettings");
            string token = context.Request.Headers[HeaderNames.Authorization];
            if(string.IsNullOrEmpty(token)) token = context.Request.Query["access_token"];
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            bool existCacheToken = false;
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                var redisCache = context.HttpContext.RequestServices?.GetService<IRedisCache>();
                if (redisCache != null)
                {
                    var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    var cacheToken = redisCache.StringGet(string.Format(CacheKeyConfig.AuthTokenKey, userId));
                    if (!string.IsNullOrEmpty(cacheToken))
                    {
                        TokenValidationParameters tokenValidationParameters = context.Options.TokenValidationParameters.Clone();
                        var securityTokenValidator = new MyJwtSecurityTokenHandler(redisCache);
                        var principal = securityTokenValidator.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                        context.Principal = principal;
                        context.Success();
                        existCacheToken = true;
                    }
                }
                context.Response.Headers.Add("Token-Expired", "true");
            }
            if (!existCacheToken)
            {
                if (jwtToken.Issuer != jwtSetting.Issuer)
                {
                    context.Response.Headers.Add("Token-Error-Issuer", "true");
                }

                if (jwtToken.Audiences.FirstOrDefault() != jwtSetting.Audience)
                {
                    context.Response.Headers.Add("Token-Error-Audience", "true");
                }
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 验证token时的事件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task TokenValidated(TokenValidatedContext context)
        {
            string token = context.Request.Headers[HeaderNames.Authorization];
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var redisCache = context.HttpContext.RequestServices?.GetService<IRedisCache>();
            if (redisCache != null)
            {
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var cacheToken = redisCache.StringGet(string.Format(CacheKeyConfig.AuthTokenKey, userId));
                if (!string.IsNullOrEmpty(cacheToken))
                {
                    context.Success();
                }
            }
            await Task.CompletedTask;
        }
    }
}
