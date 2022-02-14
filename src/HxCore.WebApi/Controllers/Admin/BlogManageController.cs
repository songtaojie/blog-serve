using Hx.Sdk.Attributes;
using Hx.Sdk.Entity.Page;
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
        public async Task<List<TagManageModel>> GetTagList()
        {
            return await _blogQuery.GetTagManageListAsync();
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

        #region 标签栏目查询
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<SqlSugarPageModel<TagManageModel>> GetTagPage(BasePageParam param)
        {
            return await _blogQuery.QueryTagPageAsync(param);
        }

        /// <summary>
        /// 获取博客标签详情数据
        /// </summary>
        /// <param name="tagId">标签id</param>
        /// <returns></returns>
        [HttpGet("{tagId}")]
        public async Task<TagManageModel> GetTagDetail(string tagId)
        {
            return await _blogQuery.GetTagDetailAsync(tagId);
        }

        /// <summary>
        /// 获取博客栏目列表
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SqlSugarPageModel<CategoryManageModel>> GetCategoryPage(BasePageParam param)
        {
            return await _blogQuery.QueryCategoryPageAsync(param);
        }

        /// <summary>
        /// 获取博客栏目详情数据
        /// </summary>
        /// <param name="categoryId">栏目id</param>
        /// <returns></returns>
        [HttpGet("{categoryId}")]
        public async Task<CategoryManageModel> GetCategoryDetail(string categoryId)
        {
            return await _blogQuery.GetCategoryDetailAsync(categoryId);
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

        #region 标签/栏目操作
        /// <summary>
        /// 添加或者更新标签
        /// </summary>
        /// <param name="tagModel">标签信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddOrUpdateTag(TagManageModel tagModel)
        {
            return await _blogService.AddOrUpdateTagAsync(tagModel);
        }

        /// <summary>
        /// 添加或者更新栏目
        /// </summary>
        /// <param name="categoryModel">栏目信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddOrUpdateCategory(CategoryManageModel categoryModel)
        {
            return await _blogService.AddOrUpdateCategoryAsync(categoryModel);
        }

        /// <summary>
        /// 博客标签
        /// </summary>
        /// <param name="tagId">要删除的标签id</param>
        /// <returns></returns>
        [HttpDelete("{tagId}")]
        public async Task<bool> DeleteTag(string tagId)
        {
            return await _blogService.DeleteTagAsync(tagId);
        }

        /// <summary>
        /// 博客博客栏目
        /// </summary>
        /// <param name="categoryId">要删除的栏目id</param>
        /// <returns></returns>
        [HttpDelete("{categoryId}")]
        public async Task<bool> DeleteCategory(string categoryId)
        {
            return await _blogService.DeleteCategoryAsync(categoryId);
        }
        #endregion
    }
}
