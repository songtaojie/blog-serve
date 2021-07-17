using Hx.Sdk.Entity.Page;
using HxCore.Entity.Entities;
using HxCore.Model.Admin.Module;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices.Admin
{
    public interface IModuleService : IBaseStatusService<T_Module>
    {
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="createModel">用户提交数据</param>
        /// <returns></returns>
        Task<bool> AddAsync(ModuleCreateModel createModel);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="createModel">用户提交数据</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(ModuleCreateModel createModel);
        /// <summary>
        /// 获取接口列表数据
        /// </summary>
        /// <returns></returns>
        Task<PageModel<ModuleQueryModel>> QueryModulePageAsync(ModuleQueryParam param);
        /// <summary>
        /// 获取接口管理详情数据
        /// </summary>
        /// <returns></returns>
        Task<ModuleDetailModel> GetAsync(string id);

    }
}
