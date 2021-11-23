using Microsoft.AspNetCore.Mvc;

namespace HxCore.WebApi.Controllers.Base
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
