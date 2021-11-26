using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity;
using HxCore.Entity.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.User
{
    /// <summary>
    /// 用户查询model
    /// </summary>
    public class UserQueryModel:IAutoMapper<T_Account>
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// 用户角色名称，多个逗号隔开
        /// </summary>
        public string UserRoleName { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [JsonIgnore]
        public string Lock { set; get; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock => Lock == ConstKey.Yes;

        /// <summary>
        /// 头像存储文件路径
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public string Activate { get; set; } 
        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 登录的ip
        /// </summary>
        public string LoginIp { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public virtual DateTime? LastLoginTime { get; set; }

    }
}
