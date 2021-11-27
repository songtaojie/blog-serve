﻿using Hx.Sdk.Attributes;
using HxCore.IServices;
using HxCore.Model.Admin.Blog;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers.Admin
{
    /// <summary>
    /// 后台管理博客管理
    /// </summary>
    public class BlogManageController : BaseAdminController
    {
        private readonly IBlogService _blogService;
        private readonly IBlogQuery _blogQuery;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="blogService"></param>
        /// <param name="blogQuery"></param>
        public BlogManageController(IBlogService blogService, IBlogQuery blogQuery)
        {
            _blogService = blogService;
            _blogQuery = blogQuery;
        }
        #region 博客查询
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<SqlSugarPageModel<BlogManageQueryModel>> GetPage(BlogManageQueryParam param)
        {
            var result = await _blogQuery.QueryBlogListAsync(param);
            return result;
        }

        /// <summary>
        /// 获取博客个人标签列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SkipRouteAuthorization]
        public async Task<List<BlogManagePersonTag>> GetTagList()
        {
            return await _blogQuery.GetTagListAsync();
        }

        /// <summary>
        /// 获取博客详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<BlogManageDetailModel> Get(string id)
        {
            return await _blogQuery.GetDetailAsync(id);
        }

        #endregion

        #region 博客操作作
        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="editInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(BlogManageCreateModel editInfo)
        {
            return await _blogService.InsertAsync(editInfo);
        }

        /// <summary>
        /// 博客编辑
        /// </summary>
        /// <param name="editInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Update(BlogManageCreateModel editInfo)
        {
            return await _blogService.UpdateAsync(editInfo);
        }

        /// <summary>
        /// 博客删除
        /// </summary>
        /// <param name="id">要删除的博客的</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _blogService.DeleteAsync(id);
        }
        #endregion
    }
}
