using System.Threading.Tasks;
using HxCore.IServices;
using Microsoft.AspNetCore.Mvc;
using HxCore.Model;
using Hx.Sdk.Entity.Page;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using SqlSugar;
using MediatR;
using HxCore.Model.NotificationHandlers;
using HxCore.Model.Client;

namespace HxCore.WebApi.Controllers.Client
{
    /// <summary>
    /// 博客相关的控制器类
    /// </summary>
    
    public class BlogController : BaseApiController
    {
        private readonly IBlogQuery _query;
        private readonly IMediator _mediator;

        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="blogQuery"></param>
        /// <param name="mediator"></param>
        public BlogController(IBlogQuery blogQuery, IMediator mediator)
        {
            _query = blogQuery;
            _mediator = mediator;
        }

        #region 博客查询
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/articles/{page}")]
        public async Task<SqlSugarPageModel<BlogQueryModel>> Articles(int page)
        {
            var result = await _query.GetBlogsAsync(new BlogQueryParam
            { 
                PageIndex = page,
            });
            return result;
        }

        /// <summary>
        /// 根据博客id获取博客信息
        /// </summary>
        /// <param name="id">博客id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/article/{id}")]
        public async Task<BlogDetailModel> Detail(string id)
        {
            //更新浏览次数
            _ = _mediator.Publish(new UpdateBlogReadModel
            {
                Id = id
            });
            return await _query.Detail(id);
        }
        #endregion 
    }
}