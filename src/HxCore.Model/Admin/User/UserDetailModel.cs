using Hx.Sdk.Entity.Dependency;
using HxCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.User
{
    /// <summary>
    /// 用户详情页model
    /// </summary>
    public class UserDetailModel : IAutoMapper<T_User>
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get; set;
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            set; get;
        }
        /// <summary>
        /// 头像存储文件路径
        /// </summary>
        public string AvatarUrl
        {
            get; set;
        }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public string Lock { set; get; }
    }
}
