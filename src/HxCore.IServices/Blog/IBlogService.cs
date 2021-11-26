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

    
    }
}
