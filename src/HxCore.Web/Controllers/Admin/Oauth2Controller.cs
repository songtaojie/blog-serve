using Hx.Sdk.Cache;
using Hx.Sdk.Common.Extensions;
using HxCore.Entity.Entities;
using HxCore.IServices;
using HxCore.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Hx.Sdk.FriendlyException;
using HxCore.Entity.Enum;
using Hx.Sdk.Common.Helper;
using System.Linq;
using HxCore.Entity;
using HxCore.IServices.Admin;
using Hx.Sdk.Core;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using HxCore.Extensions.Common;
using HxCore.Extensions.Authentication;
using Hx.Sdk.Attributes;

namespace HxCore.Web.Controllers.Admin
{
    /// <summary>
    /// 账户授权相关的控制器类
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiDescriptionSettings(GroupName = "Admin", Groups = new string[] { "Admin" })]
    [SkipOperateLog]
    public class Oauth2Controller : ControllerBase
    {
        /// <summary>
        /// 用户服务类
        /// </summary>
        private readonly IUserService _userService;
        private readonly IRedisCache _redisCache;
        private readonly IRoleService _roleService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService">用户</param>
        /// <param name="roleService">角色</param>
        /// <param name="redisCache">缓存</param>
        public Oauth2Controller(IUserService userService, IRoleService roleService, IRedisCache redisCache)
        {
            _userService = userService;
            _redisCache = redisCache;
            _roleService = roleService;
            _redisCache.SetRedisDbNum(1);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="param">密码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<LoginVm> Authorize(LoginParam param)
        {
            string md5pwd = param.PassWord.MD5TwoEncrypt();
            T_User userInfo = await _userService.FirstOrDefaultAsync(u => u.UserName == param.UserName && u.PassWord == md5pwd);
            if (userInfo == null) throw new UserFriendlyException("用户名或密码错误", ErrorCodeEnum.Error);
            await _userService.UpdateLoginInfoAsync(userInfo.Id);
            LoginVm result = await BuildJwtResult(userInfo);
            if (param.Remember ?? false)
            {
                await _redisCache.StringSetAsync(string.Format(CacheKeyConfig.AuthTokenKey, userInfo.Id), result.AccessToken,TimeSpan.FromDays(7));
            }
            return result;
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<bool> Logout()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier)?.Value;
            var tokenKey = string.Format(CacheKeyConfig.AuthTokenKey, userId);
            await _redisCache.RemoveAsync(tokenKey);
            //移除菜单缓存
            var menuKey = string.Format(CacheKeyConfig.AuthRouterKey, userId);
            await _redisCache.RemoveAsync(menuKey);
            return true;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="token">旧的token</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<LoginVm> RefreshToken([FromForm]string token)
        {
            if (string.IsNullOrEmpty(token)) throw new UserFriendlyException("token无效，请重新登录！", ErrorCodeEnum.NoAccessError);
            var tokenModel = JwtHelper.SerializeJwt(token);
            T_User userInfo = await _userService.FindAsync(tokenModel.UserId);
            if (userInfo == null) throw new UserFriendlyException("token解析失败，请重新登录！", ErrorCodeEnum.NoAccessError);
            LoginVm result = await BuildJwtResult(userInfo);
            return result;
        }

        private async Task<LoginVm> BuildJwtResult(T_User userInfo)
        {
            JwtModel jwtModel = new JwtModel
            {
                IsSuperAdmin = Helper.IsYes(userInfo.SuperAdmin),
                UserId = userInfo.Id,
                NickName = userInfo.NickName,
                UserName = userInfo.UserName,
                Expiration = TimeSpan.FromSeconds(1 * 60),
            };
            var roleList = await _roleService.GetListByUserAsync(userInfo.Id);
            if (roleList.Count > 0)
            {
                jwtModel.RoleId = string.Join(",", roleList.Select(r => r.Id));
                jwtModel.Role = string.Join(",", roleList.Select(r => r.Code));
            }
            var result = JwtHelper.BuildJwtToken(jwtModel);
            result.NickName = userInfo.NickName;
            result.AvatarUrl = userInfo.AvatarUrl;
            result.IsUseMdEdit = Hx.Sdk.Common.Helper.Helper.IsYes(userInfo.UseMdEdit);
            return result;
        }
    }
}
