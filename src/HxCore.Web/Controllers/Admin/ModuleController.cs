using Hx.Sdk.Entity.Page;
using HxCore.IServices.Admin;
using HxCore.Model.Admin.Module;
using HxCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HxCore.Web.Controllers.Admin
{
    /// <summary>
    /// 接口api控制器
    /// </summary>
    public class ModuleController : BaseAdminController
    {
        private readonly IModuleService _service;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service"></param>
        public ModuleController(IModuleService service)
        {
            _service = service;
        }

        #region 查询
        /// <summary>
        /// 获取接口列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<ModuleQueryModel>> GetPageAsync(ModuleQueryParam param)
        {
            var result = await _service.QueryModulePageAsync(param);
            return result;
        }

        /// <summary>
        /// 获取接口列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ModuleDetailModel> GetAsync(string id)
        {
            var result = await _service.GetAsync(id);
            return result;
        }
        #endregion

        #region 操作
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddAsync(ModuleCreateModel createModel)
        {
            return await _service.AddAsync(createModel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAsync(ModuleCreateModel createModel)
        {
            return await _service.UpdateAsync(createModel);
        }

        /// <summary>
        /// 接口删除
        /// </summary>
        /// <param name="id">要删除的接口的id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(string id)
        {
            return await _service.DeleteAsync(id);
        }
        #endregion
    }
}
