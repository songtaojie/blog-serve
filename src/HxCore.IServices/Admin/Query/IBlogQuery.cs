﻿using Hx.Sdk.Entity.Page;
using HxCore.Model;
using HxCore.Model.Admin.Blog;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    public interface IBlogQuery
    {
        #region 客户端
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<BlogQueryModel>> GetBlogsAsync(Model.BlogQueryParam param);

        /// <summary>
        /// 获取博客详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BlogDetailModel> Detail(string id);
        #endregion

        #region 管理后台

        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<BlogManageQueryModel>> QueryBlogListAsync(Model.Admin.Blog.BlogManageQueryParam param);

        /// <summary>
        /// 获取博客详情-后台管理
        /// </summary>
        /// <param name="id">博客id</param>
        /// <returns></returns>
        Task<BlogManageDetailModel> GetDetailAsync(string id);

        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        Task<List<BlogManagePersonTag>> GetTagListAsync();

        #endregion

        #region 标签/栏目-后台管理
        /// <summary>
        /// 获取博客标签列表数据
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<TagManageModel>> QueryTagPageAsync(BasePageParam param);

        /// <summary>
        /// 获取博客标签详情数据
        /// </summary>
        /// <param name="tagId">标签id</param>
        /// <returns></returns>
        Task<TagManageModel> GetTagDetailAsync(string tagId);

        /// <summary>
        /// 获取博客栏目列表数据
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<CategoryManageModel>> QueryCategoryPageAsync(BasePageParam param);

        /// <summary>
        /// 获取栏目详情数据
        /// </summary>
        /// <param name="categoryId">栏目id</param>
        /// <returns></returns>
        Task<CategoryManageModel> GetCategoryDetailAsync(string categoryId);
        #endregion 
    }
}
