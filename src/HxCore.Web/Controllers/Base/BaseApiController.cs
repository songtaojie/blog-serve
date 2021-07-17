using AutoMapper;
using HxCore.Entity.Context;
using HxCore.Extensions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HxCore.Web.Controllers.Base
{
    /// <summary>
    /// 基础的api
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiDescriptionSettings(GroupName = "Client", Groups = new string[] { "Client" })]
    public class BaseApiController: ControllerBase
    {
        
    }
}
