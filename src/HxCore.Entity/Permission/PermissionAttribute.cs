using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Entity.Permission
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PermissionAttribute : Attribute//, IAuthorizationFilter
    {
        public string Name { get; set; }

        public PermissionAttribute(string name)
        {
            Name = name;
        }

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    var permissionHandlerProvider = context.HttpContext.RequestServices.GetRequiredService<IPermissionHandlerProvider>();
        //    var permissionHandler = permissionHandlerProvider.GetHandler(context);
        //    var hasPermission = permissionHandler.HasPermission(context.HttpContext.User, this.Name);
        //    if (hasPermission == false)
        //    {
        //        context.Result = new JsonResult(new Hx.Sdk.UnifyResult.RESTfulResult<object>
        //        { 
                
                    
        //        })
        //        { 
        //            StatusCode = StatusCodes.Status200OK
        //        };
        //    }
        //}
    }
}
