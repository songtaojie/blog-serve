using Hx.Sdk.Entity.Page;
using HxCore.Extensions.Common;
using HxCore.IServices;
using HxCore.Model.Admin;
using HxCore.Model.Admin.Blog;
using HxCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.Web.Controllers.Admin
{
    /// <summary>
    /// 后台管理博客管理
    /// </summary>
    public class BlogManageController : BaseAdminController
    {
        private readonly IBlogService _blogService;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="blogService"></param>
        public BlogManageController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        #region 博客查询
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<BlogManageQueryModel>> GetPageAsync(BlogManageQueryParam param)
        {
            var result = await _blogService.QueryBlogListAsync(param);
            return result;
        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SkipRouteAuthorization]
        public async Task<List<BlogManagePersonTag>> GetTagListAsync()
        {
            return await _blogService.GetTagListAsync();
        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<BlogManageDetailModel> GetAsync(string id)
        {
            return await _blogService.GetDetailAsync(id);
        }

        #endregion

        #region 博客操作作
        /// <summary>
        /// 博客的编辑
        /// </summary>
        /// <param name="editInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddAsync(BlogManageCreateModel editInfo)
        {
            return await _blogService.InsertAsync(editInfo);
        }

        /// <summary>
        /// 博客的编辑
        /// </summary>
        /// <param name="editInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAsync(BlogManageCreateModel editInfo)
        {
            return await _blogService.UpdateAsync(editInfo);
        }

        /// <summary>
        /// 博客的编辑
        /// </summary>
        /// <param name="id">要删除的博客的</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(string id)
        {
            return await _blogService.DeleteAsync(id);
        }
        #endregion
    }
}
