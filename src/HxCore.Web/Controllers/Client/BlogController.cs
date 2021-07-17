using System.Threading.Tasks;
using HxCore.IServices;
using Microsoft.AspNetCore.Mvc;
using HxCore.Model;
using Hx.Sdk.Entity.Page;
using HxCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

namespace HxCore.Web.Controllers
{
    /// <summary>
    /// 博客相关的控制器类
    /// </summary>
    [AllowAnonymous]
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="blogService"></param>
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        #region 博客查询
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<BlogQueryModel>> QueryBlogsAsync(BlogQueryParam param)
        {
            var result = await _blogService.QueryBlogsAsync(param);
            return result;
        }

        /// <summary>
        /// 根据博客id获取博客信息
        /// </summary>
        /// <param name="id">博客id</param>
        /// <returns></returns>
        [HttpPost]
        public Task<BlogDetailModel> FindById(string id)
        {
            return _blogService.FindById(id);
        }
        #endregion 
    }
}