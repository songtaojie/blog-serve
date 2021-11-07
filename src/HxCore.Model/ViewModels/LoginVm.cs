using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity;
using HxCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HxCore.Model.ViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginVm: IAutoMapper<T_User>
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public double ExpiresIn { get; set; }

        /// <summary>
        /// token的前缀
        /// </summary>
        public string TokenType { get;} = "Bearer";

        /// <summary>
        /// 用户的id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像链接
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 使用MarkDown编辑器
        /// </summary>
        public bool IsUseMdEdit { get; set; }
    }
}
