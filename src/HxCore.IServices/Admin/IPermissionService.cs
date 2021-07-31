using HxCore.Entity.Entities;
using HxCore.Model.Admin.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.IServices.Admin
{
    public interface IPermissionService : IBaseStatusService<T_Menu>
    {
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="createModel">用户提交数据</param>
        /// <returns></returns>
        Task<bool> AddAsync(MenuCreateModel createModel);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="createModel">用户提交数据</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(MenuUpdateModel createModel);
        /// <summary>
        /// 获取角色详情数据
        /// </summary>
        /// <returns></returns>
        Task<MenuDetailModel> GetAsync(string id);

        /// <summary>
        /// 获取菜单列表数据
        /// </summary>
        /// <returns></returns>
        Task<List<MenuQueryModel>> GetListAsync();

        /// <summary>
        /// 获取菜单路由树
        /// </summary>
        /// <returns></returns>
        Task<List<RouterQueryModel>> GetRoutersAsync();

        /// <summary>
        /// 获取当前用户所拥有的的菜单列表
        /// </summary>
        /// <returns></returns>
        Task<List<MenuPullDownModel>> GetUserMenuTreeAsync();
    }
}
