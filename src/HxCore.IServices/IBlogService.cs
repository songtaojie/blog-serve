using Hx.Sdk.Entity.Dependency;
using Hx.Sdk.Entity.Page;
using HxCore.Entity.Entities;
using HxCore.Model;
using HxCore.Model.Admin.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    public interface IBlogService: IBaseStatusService<T_Blog>
    {
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="blogModel"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(BlogManageCreateModel blogModel);

        /// <summary>
        /// 更新博客数据
        /// </summary>
        /// <param name="blogModel">提交的数据</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BlogManageCreateModel blogModel);
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        Task<PageModel<Model.BlogQueryModel>> QueryBlogsAsync(Model.BlogQueryParam param);

        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        Task<PageModel<Model.Admin.Blog.BlogManageQueryModel>> QueryBlogListAsync(Model.Admin.Blog.BlogManageQueryParam param);
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        Task<List<BlogManagePersonTag>> GetTagListAsync();

        /// <summary>
        /// 获取博客详情-后台管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BlogManageDetailModel> GetDetailAsync(string id);


        Task<BlogDetailModel> FindById(string id);
    }
}
