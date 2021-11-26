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
        Task<BlogDetailModel> FindById(string id);
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
    }
}
