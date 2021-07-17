using Hx.Sdk.FriendlyException;
using HxCore.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.User
{
    /// <summary>
    /// 管理员修改密码的model
    /// </summary>
    public class ChangePwdModel
    {
        /// <summary>
        /// 用户的id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassWord
        {
            set; get;
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        public virtual void VerifyParam()
        {
            if (string.IsNullOrWhiteSpace(NewPassWord))
                throw new UserFriendlyException("用户密码不能为空", ErrorCodeEnum.ParamsInValidError);
           
        }
    }

    /// <summary>
    /// 我自己修改密码的model
    /// </summary>
    public class ChangeMyPwdModel
    {
        /// <summary>
        /// 老密码
        /// </summary>
        public string PassWord
        {
            set; get;
        }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassWord
        {
            set; get;
        }
        /// <summary>
        /// 验证参数
        /// </summary>
        public virtual void VerifyParam()
        {
            if (string.IsNullOrWhiteSpace(PassWord))
                throw new UserFriendlyException("原密码不能为空", ErrorCodeEnum.ParamsInValidError);
            if (string.IsNullOrWhiteSpace(NewPassWord))
                throw new UserFriendlyException("新密码不能为空", ErrorCodeEnum.ParamsInValidError);

        }
    }
}
