using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Entity.Permission
{
    /// <summary>
    /// 权限提供器
    /// </summary>
    public interface IPermissionHandlerProvider
    {
        IPermissionHandler GetHandler(AuthorizationFilterContext context);
    }
}
