using HxCore.Extensions.Common;
using Microsoft.AspNetCore.Authorization;

namespace HxCore.WebApi.Controllers.Base
{
    /// <summary>
    /// 客户端使用的api
    /// </summary>
    [Authorize(Policy = ConstInfo.ClientPolicy)]
    public abstract class BaseAuthApiController: BaseApiController
    {
       
    }
}
