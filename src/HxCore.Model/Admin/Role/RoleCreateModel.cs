using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Entities;
using System;
using System.Collections.Generic;

namespace HxCore.Model.Admin.Role
{
    /// <summary>
    /// 角色添加或者编辑model
    /// </summary>
    public class RoleCreateModel:IAutoMapper<T_Role>
    {
        /// <summary>
        /// 角色的id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// /描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 给角色分配权限
    /// </summary>
    public class AssignPermissionModel
    {
        /// <summary>
        /// 角色的id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        public List<string> MenuIds { get; set; }

        /// <summary>
        /// 校验入参
        /// </summary>
        /// <returns></returns>
        public virtual void VerifyParam()
        {
            if (string.IsNullOrWhiteSpace(RoleId))
                throw new ArgumentException("角色标识不能为空");
            if (MenuIds == null || MenuIds.Count == 0)
                throw new ArgumentException("权限不能为空");
        }
    }
}
