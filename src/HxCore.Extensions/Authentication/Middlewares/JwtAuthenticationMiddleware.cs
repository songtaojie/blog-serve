﻿using HxCore.Extensions.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HxCore.Extensions.Middlewares
{
    /// <summary>
    /// jwt中间件
    /// </summary>
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">下一个管道</param>
        public JwtAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                 await _next(context);
            }
            string tokenHeader = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                if (tokenHeader.Length >= 128)
                {
                    JwtModel model = JwtHelper.SerializeJwt(tokenHeader);
                    var claimList = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role,model.Role)
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claimList);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    context.User = principal;
                }
            }
            catch
            {

            }
            await _next(context);
        }
    }
}
