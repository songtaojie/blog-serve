using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.Entity.Page;
using HxCore.Entity.Entities;
using HxCore.Model.Admin.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices.Admin
{
    public interface IRoleService : IBaseStatusService<T_Role>
    {
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="createModel">用户提交数据</param>
        /// <returns></returns>
        Task<bool> AddAsync(RoleCreateModel createModel);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="createModel">用户提交数据</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(RoleCreateModel createModel);
        /// <summary>
        /// 给角色分配权限
        /// </summary>
        /// <param name="model">用户提交数据</param>
        /// <returns></returns>
        Task<bool> AssignPermission(AssignPermissionModel model);

        /// <summary>
        /// 获取角色列表数据
        /// </summary>
        /// <returns></returns>
        Task<PageModel<RoleQueryModel>> QueryRolePageAsync(RoleQueryParam param);
        /// <summary>
        /// 获取角色详情数据
        /// </summary>
        /// <returns></returns>
        Task<RoleDetailModel> GetAsync(string id);

        /// <summary>
        /// 获取角色的权限数据
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns></returns>
        Task<List<string>> GetPermissionsAsync(string id);

        /// <summary>
        /// 获取角色列表数据
        /// </summary>
        /// <returns></returns>
        Task<List<RoleQueryModel>> GetListAsync();

        /// <summary>
        /// 获取某个用户的角色数据
        /// </summary>
        /// <param name="accountId">账号id</param>
        /// <returns></returns>
        Task<List<RoleQueryModel>> GetListByUserAsync(string accountId);

        /// <summary>
        /// 获取所有的角色菜单
        /// </summary>
        /// <returns></returns>
        Task<List<RoleMenuModel>> GetAllRoleMenusAsync();

        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <param name="accountId">账号id</param>
        /// <returns></returns>
        Task<List<Model.Admin.User.UserRoleModel>> GetUserRoleAsync(string accountId);

        /// <summary>
        /// 检查用户是否是超级管理员
        /// </summary>
        /// <param name="accountId">账号id</param>
        /// <returns></returns>
        Task<bool> CheckIsSuperAdminAsync(string accountId);
    }
}
