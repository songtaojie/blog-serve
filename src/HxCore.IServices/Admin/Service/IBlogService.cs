using Hx.Sdk.DatabaseAccessor;
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
        /// 添加博客
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
        /// 更新博客数据
        /// </summary>
        /// <param name="model">提交的数据</param>
        /// <returns></returns>
        Task<bool> UpdateBlogReadAsync(Model.NotificationHandlers.UpdateBlogReadModel model);

        /// <summary>
        /// 添加或更新博客标签
        /// </summary>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        Task<bool> AddOrUpdateTagAsync(TagManageModel tagModel);

        /// <summary>
        /// 添加或更新博客分类
        /// </summary>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        Task<bool> AddOrUpdateCategoryAsync(CategoryManageModel tagModel);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tagId">标签id</param>
        /// <returns></returns>
        Task<bool> DeleteTagAsync(string tagId);

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="categoryId">栏目id</param>
        /// <returns></returns>
        Task<bool> DeleteCategoryAsync(string categoryId);



    }
}
