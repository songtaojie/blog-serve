using Hx.Sdk.Entity.Dependency;
using HxCore.Entity.Entities;
using HxCore.Entity.Enum;
using Hx.Sdk.FriendlyException;

namespace HxCore.Model.Admin.User
{
    /// <summary>
    ///用户的添加或者编辑model
    /// </summary>
    public class UserCreateModel : IAutoMapper<T_User>
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
        /// 密码
        /// </summary>
        public string PassWord
        {
            set;get;
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
        /// 验证参数
        /// </summary>
        public virtual void VerifyParam()
        {
            if (string.IsNullOrWhiteSpace(UserName))
                throw new UserFriendlyException("用户名称不能为空", ErrorCodeEnum.ParamsInValidError);
            if (string.IsNullOrWhiteSpace(Email))
                throw new UserFriendlyException("邮箱不能为空", ErrorCodeEnum.ParamsInValidError);
            if (string.IsNullOrWhiteSpace(PassWord))
                throw new UserFriendlyException("密码不能为空", ErrorCodeEnum.ParamsInValidError);
            if (string.IsNullOrWhiteSpace(NickName))
                throw new UserFriendlyException("昵称不能为空", ErrorCodeEnum.ParamsInValidError);
        }
    }
}
