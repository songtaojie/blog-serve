using Hx.Sdk.FriendlyException;
using HxCore.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HxCore.Model.Admin.User
{
    /// <summary>
    /// 分配角色的model
    /// </summary>
    public class AssignRoleModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色id集合
        /// </summary>
        public List<string> RoleIds { get; set; }

        /// <summary>
        /// 验证参数
        /// </summary>
        public virtual void VerifyParam()
        {
            if (string.IsNullOrWhiteSpace(UserId))
                throw new UserFriendlyException("用户标识不能为空", ErrorCodeEnum.ParamsInValidError);
            if (RoleIds == null || !RoleIds.Any())
                throw new UserFriendlyException("角色不能为空", ErrorCodeEnum.ParamsInValidError);

        }
    }
}
