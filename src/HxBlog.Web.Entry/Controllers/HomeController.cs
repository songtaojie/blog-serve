using HxBlog.Core;
using HxBlog.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HxBlog.Web.Entry.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly SysConfigService _sysConfigService;

        public HomeController(SysConfigService sysConfigService)
        {
            _sysConfigService = sysConfigService;
        }

        public IActionResult Index()
        {
            ViewBag.Description = _sysConfigService.GetConfigValue<string>(CommonConst.SysDescription);

            return View();
        }
    }
}