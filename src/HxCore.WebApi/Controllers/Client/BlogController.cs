using System.Threading.Tasks;
using HxCore.IServices;
using Microsoft.AspNetCore.Mvc;
using HxCore.Model;
using Hx.Sdk.Entity.Page;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using SqlSugar;

namespace HxCore.WebApi.Controllers
{
    /// <summary>
    /// 博客相关的控制器类
    /// </summary>
    [AllowAnonymous]
    public class BlogController : BaseApiController
    {
        private readonly IBlogQuery _blogQuery;

        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="blogQuery"></param>
        public BlogController(IBlogQuery blogQuery)
        {
            _blogQuery = blogQuery;
        }
        #region 博客查询
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/articles")]
        public async Task<SqlSugarPageModel<BlogQueryModel>> GetPageAsync(BlogQueryParam param)
        {
            var result = await _blogQuery.GetBlogsAsync(param);
            return result;
        }

        /// <summary>
        /// 根据博客id获取博客信息
        /// </summary>
        /// <param name="id">博客id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/article/{id}")]
        public Task<BlogDetailModel> FindById(string id)
        {
            return _blogQuery.FindById(id);
        }
        #endregion 
    }
}