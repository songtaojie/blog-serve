using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace HxBlog.Entity.Permission
{
    /// <summary>
    /// 权限处理程序
    /// </summary>
    public interface IPermissionHandler
    {
        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        bool HasPermission(ClaimsPrincipal principal, string permissionName);
    }
}
