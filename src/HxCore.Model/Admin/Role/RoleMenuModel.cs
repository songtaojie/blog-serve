using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.Role
{
    /// <summary>
    /// 角色菜单权限的model
    /// </summary>
    public class RoleMenuModel
    {
        /// <summary>
        ///菜单列表
        /// </summary>
        public List<RoleMenuSelectModel> Menus { get; set; } = new List<RoleMenuSelectModel>();

        /// <summary>
        /// 需要选中的菜单ID
        /// </summary>
        public List<string> CheckedKeys { get; set; } = new List<string>(0);
    }

    /// <summary>
    /// 树列表
    /// </summary>
    public class RoleMenuSelectModel : BaseTreeModel<RoleMenuSelectModel>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; } = string.Empty;
    }
}
