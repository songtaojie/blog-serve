﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace HxCore.Entity.Permission
{
    public class RolePermissionHandler : IPermissionHandler
    {
        public bool HasPermission(ClaimsPrincipal principal, string permissionName)
        {

            var userId = principal.FindFirstValue("sub");
            //todo:自定义部分，通过用户Id获取到到其拥有那些Permission
            return userId == "000001";
        }
    }
}
