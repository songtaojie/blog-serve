using HxCore.Entity.Entities;
using HxCore.Model.Admin.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.IServices.Admin
{
    public interface IPermissionService : IBaseStatusService<T_Menu>
    {
        /// <summary>
        /// 插入菜单/按钮
        /// </summary>
        /// <param name="createModel">用户提交数据</param>
        /// <returns></returns>
        Task<bool> AddAsync(MenuCreateModel createModel);
        /// <summary>
        /// 更新菜单/按钮
        /// </summary>
        /// <param name="createModel">用户提交数据</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(MenuUpdateModel createModel);
        /// <summary>
        /// 获取菜单/按钮详情数据
        /// </summary>
        /// <returns></returns>
        Task<MenuDetailModel> GetAsync(string id);

        /// <summary>
        /// 删除菜单/按钮
        /// </summary>
        /// <param name="id">菜单/按钮id</param>
        /// <returns></returns>
        Task<bool> DeleteMenuAsync(string id);

        /// <summary>
        /// 获取菜单/按钮列表数据
        /// </summary>
        /// <returns></returns>
        Task<List<MenuQueryModel>> GetListAsync();

        /// <summary>
        /// 获取菜单路由树
        /// </summary>
        /// <returns></returns>
        Task<UserRouterModel> GetRoutersAsync();

        /// <summary>
        /// 获取当前用户所拥有的的菜单列表
        /// </summary>
        /// <returns></returns>
        Task<List<MenuPullDownModel>> GetUserMenuTreeAsync();

        /// <summary>
        /// 获取用户权限数据
        /// </summary>
        /// <returns></returns>
        Task<UserPermissionCached> GetUserPermissionAsync();
    }
}
