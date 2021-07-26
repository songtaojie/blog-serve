using Hx.Sdk.Cache;
using Hx.Sdk.Entity;
using Hx.Sdk.UnifyResult;
using HxCore.Entity;
using HxCore.Extensions.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HxCore.Extensions.Auth
{
    public class MyJwtBearerHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public MyJwtBearerHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) 
            : base(options, logger, encoder, clock)
        {
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 如果有问题的身份验证方案将身份验证交互作为其请求流的一部分来处理，则重写此方法以处理401个挑战问题。
        /// (比如添加一个响应标头，或者将登录页面或外部登录位置的401结果更改为302。)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = StatusCodes.Status200OK;
            base.Response.Headers.Append(HeaderNames.WWWAuthenticate, nameof(MyJwtBearerHandler));
            Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings()
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            await Response.WriteAsync(JsonConvert.SerializeObject(new RESTfulResult<object>
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Succeeded = false,
                Data = null,
                Message = "401 Unauthorized",
                Extras = UnifyResultContext.Take(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            }, setting));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            base.Response.ContentType = "application/json";
            base.Response.StatusCode = StatusCodes.Status200OK;
            base.Response.Headers.Append(HeaderNames.WWWAuthenticate, nameof(MyJwtBearerHandler));
            Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings()
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            await Response.WriteAsync(JsonConvert.SerializeObject(new RESTfulResult<object>
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Succeeded = false,
                Data = null,
                Message = "403 Forbidden",
                Extras = UnifyResultContext.Take(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            }, setting));
        }
    }


    
}
