using HxCore.Model.Admin.User;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 用户查询接口
    /// </summary>
    public interface IUserQuery
    {
        /// <summary>
        /// 获取详情数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        Task<UserDetailModel> GetAsync(string id);

        /// <summary>
        /// 获取当前用户详情数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        Task<UserDetailModel> GetCurrentUserInfoAsync();

        /// <summary>
        /// 获取用户所拥有的的角色信息
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        Task<List<UserRoleModel>> GetRoleByIdAsync(string accountId);

        /// <summary>
        /// 获取用户列表数据
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<UserQueryModel>> QueryUserPageAsync(UserQueryParam param);
    }
}
