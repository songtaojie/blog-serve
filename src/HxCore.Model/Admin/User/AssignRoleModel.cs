﻿using Hx.Sdk.FriendlyException;
using HxCore.Enums;
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
        ///账户id
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// 角色id集合
        /// </summary>
        public List<string> RoleIds { get; set; }

        /// <summary>
        /// 验证参数
        /// </summary>
        public virtual void VerifyParam()
        {
            if (string.IsNullOrWhiteSpace(AccountId))
                throw new UserFriendlyException("账户标识不能为空", ErrorCodeEnum.ParamsInValidError);
            if (RoleIds == null || !RoleIds.Any())
                throw new UserFriendlyException("角色不能为空", ErrorCodeEnum.ParamsInValidError);

        }
    }
}
