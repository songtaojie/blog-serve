using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.Menu
{
    /// <summary>
    /// 用户的权限的缓存
    /// </summary>
    public class UserPermissionCached
    {
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; } = false;
        /// <summary>
        /// 用户所拥有的的角色
        /// </summary>
        public List<string> RoleIds { get; set; }
        /// <summary>
        /// 用户的菜单路由权限
        /// </summary>
        public List<RouterQueryModel> Routers { get; set; }
    }
}
