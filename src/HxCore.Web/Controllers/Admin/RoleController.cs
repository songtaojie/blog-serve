using Hx.Sdk.Core;
using Hx.Sdk.Entity.Page;
using HxCore.IServices.Admin;
using HxCore.IServices.Ids4;
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
        private readonly IRoleService _roleService;

        private readonly IIds4RoleService _ids4RoleService;

        private readonly bool IsUseIds4 = App.Settings.UseIdentityServer4 ?? false;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="roleService">不使用IdentityServer4时的角色服务</param>
        /// <param name="ids4RoleService">使用IdentityServer时的角色服务</param>
        public RoleController(IRoleService roleService, IIds4RoleService ids4RoleService)
        {
            _roleService = roleService;
            _ids4RoleService = ids4RoleService;
        }


        #region 查询
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<RoleQueryModel>> GetPage(RoleQueryParam param)
        {
            if (IsUseIds4)
            {
                return await _ids4RoleService.QueryRolePageAsync(param);
            }
            return await _roleService.QueryRolePageAsync(param);
        }

        /// <summary>
        /// 获取角色列表数据（用于下拉框选择角色）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleQueryModel>> GetList()
        {
            return await _roleService.GetListAsync();
        }

        /// <summary>
        /// 获取角色详情数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RoleDetailModel> Get(string id)
        {
            return await _roleService.GetAsync(id);
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<List<string>> GetPermissions(string id)
        {
            return await _roleService.GetPermissionsAsync(id);
        }

        #endregion

        #region 本地角色数据操作,Ids4角色数据的维护在Ids4服务器上进行维护
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(RoleCreateModel createModel)
        {
            return await _roleService.AddAsync(createModel);
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Update(RoleCreateModel createModel)
        {
            return await _roleService.UpdateAsync(createModel);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">要删除的接口的id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _roleService.DeleteAsync(id);
        }
        #endregion

        /// <summary>
        /// 给角色分配权限
        /// </summary>
        /// <param name="model">用户提交的数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AssignPermission(AssignPermissionModel model)
        {
            return await _roleService.AssignPermission(model);
        }
    }
}
