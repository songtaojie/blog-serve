using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.User
{
    /// <summary>
    ///用户编辑model
    /// </summary>
    public class UserUpdateModel : IAutoMapper<T_User>
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
        public string Lock
        {
            get; set;
        }

        /// <summary>
        /// 使用MarkDown编辑器
        /// </summary>
        public string UseMdEdit { get; set; } = ConstKey.No;

        /// <summary>
        /// 验证参数
        /// </summary>
        public virtual void VerifyParam()
        {
            if (string.IsNullOrWhiteSpace(UserName))
                throw new UserFriendlyException("用户名称不能为空", ErrorCodeEnum.ParamsInValidError);
            if (string.IsNullOrWhiteSpace(Email))
                throw new UserFriendlyException("邮箱不能为空", ErrorCodeEnum.ParamsInValidError);
            if (string.IsNullOrWhiteSpace(NickName))
                throw new UserFriendlyException("昵称不能为空", ErrorCodeEnum.ParamsInValidError);
        }
    }
}
