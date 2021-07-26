using Hx.Sdk.Entity.Page;
using HxCore.IServices.Admin;
using HxCore.Model.Admin.Role;
using HxCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.Web.Controllers.Admin
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    public class RoleController:BaseAdminController
    {
        private readonly IRoleService _service;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service"></param>
        public RoleController(IRoleService service)
        {
            _service = service;
        }


        #region 查询
        /// <summary>
        /// 获取接口列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<RoleQueryModel>> GetPageAsync(RoleQueryParam param)
        {
            var result = await _service.QueryRolePageAsync(param);
            return result;
        }

        /// <summary>
        /// 获取接口列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleQueryModel>> GetListAsync()
        {
            return await _service.GetListAsync();
        }

        /// <summary>
        /// 获取接口列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RoleDetailModel> GetAsync(string id)
        {
            return await _service.GetAsync(id);
        }

        #endregion

        #region 操作
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddAsync(RoleCreateModel createModel)
        {
            return await _service.AddAsync(createModel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAsync(RoleCreateModel createModel)
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
