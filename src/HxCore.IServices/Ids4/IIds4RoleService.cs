using HxCore.Entity.Context;
using HxCore.Entity.Entities.Ids4;
using HxCore.Model.Admin.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.IServices.Ids4
{
    /// <summary>
    /// IDS4的角色服务器
    /// </summary>
    public interface IIds4RoleService: IBaseService<ApplicationRole, IdsDbContextLocator>
    {
        /// <summary>
        /// 检查是否是超级管理员
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckIsSuperAdminAsync(string userId);

        /// <summary>
        /// 获取用户的角色信息
        /// </summary>
        /// <param name="userId">用户的id</param>
        /// <returns></returns>
        Task<List<UserRoleModel>> GetUserRoleAsync(string userId);
    }
}
