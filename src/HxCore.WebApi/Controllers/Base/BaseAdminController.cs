using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HxCore.Extensions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HxCore.WebApi.Controllers.Base
{
    /// <summary>
    /// admin基础控制器
    /// </summary>
    [Route("admin/api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = ConstInfo.PermissionPolicy)]
    [ApiDescriptionSettings(GroupName = "Admin", Groups = new string[] { "Admin" })]
    public class BaseAdminController : ControllerBase
    {

    }
}
