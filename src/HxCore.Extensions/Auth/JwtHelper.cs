using Hx.Sdk.ConfigureOptions;
using HxCore.Model.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HxCore.Extensions.Auth
{
    /// <summary>
    /// jwt的帮助类
    /// </summary>
    public class JwtHelper
    {

        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns></returns>
        public static LoginVm BuildJwtToken(JwtModel model)
        {
            JwtSettings settings = AppSettings.GetConfig<JwtSettings>("JwtSettings");
            var now = DateTime.Now;
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.GivenName, model.NickName),
                new Claim(ClaimTypes.NameIdentifier, model.UserId),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(model.Expiration.TotalSeconds).ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,settings.Audience)
            };
            if (!string.IsNullOrEmpty(model.Role))
            {
                claims.AddRange(model.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
            }
            if (!string.IsNullOrEmpty(model.RoleId))
            {
                claims.AddRange(model.RoleId.Split(',').Select(s => new Claim("roleId", s)));
            }
            // 实例化JwtSecurityToken
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(model.Expiration),
                signingCredentials: creds
            );
            // 生成 Token
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //打包返回前台
            var responseJson = new LoginVm
            {
                UserId = model.UserId,
                UserName = model.UserName,
                AccessToken = encodedJwt,
                ExpiresIn = model.Expiration.TotalSeconds,
            };
            return responseJson;
        }

        /// <summary>
        /// 序列化jwt字符串
        /// </summary>
        /// <param name="jwtStr">jwt字符串</param>
        /// <returns>序列化后的jwt实体类</returns>
        public static JwtModel SerializeJwt(string jwtStr)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            try
            {
                JwtModel model = new JwtModel();
                JwtSecurityToken jwtToken = handler.ReadJwtToken(jwtStr);
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out object role);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                model.UserId = userId;
                model.Role = role.ToString();
                return model;
            }
            catch
            {
                throw;
            }
        }
    }
}
