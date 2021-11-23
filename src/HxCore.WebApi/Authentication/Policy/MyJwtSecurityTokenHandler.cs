using Hx.Sdk.Cache;
using HxCore.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HxCore.Extensions.Authentication
{
    /// <summary>
    /// token处理
    /// </summary>
    public class MyJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
        private IRedisCache _redisCache;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="redisCache"></param>
        public MyJwtSecurityTokenHandler(IRedisCache redisCache)
        {
            _redisCache = redisCache;
        }
        /// <summary>
        /// 验证过期处理
        /// </summary>
        /// <param name="notBefore"></param>
        /// <param name="expires"></param>
        /// <param name="jwtToken"></param>
        /// <param name="validationParameters"></param>
        protected override void ValidateLifetime(DateTime? notBefore, DateTime? expires, JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
        {
            try
            {
                base.ValidateLifetime(notBefore, expires, jwtToken, validationParameters);
            }
            catch (SecurityTokenExpiredException)
            {
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var cacheToken = _redisCache.StringGet(string.Format(CacheKeyConfig.AuthTokenKey, userId));
                if (string.IsNullOrEmpty(cacheToken))
                {
                    throw;
                }
            }
        }
    }
}
