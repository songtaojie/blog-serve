using Hx.Sdk.Entity.Page;
using HxCore.Entity.Entities;
using HxCore.Model.Admin.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices.Admin
{

    public interface IUserService : IBaseStatusService<T_User>
    {
        /// <summary>
        /// 获取详情数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        Task<UserDetailModel> GetAsync(string id);

        /// <summary>
        /// 获取用户所拥有的的角色信息
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        Task<UserDetailModel> GetRoleByIdAsync(string userId);

        /// <summary>
        /// 获取用户列表数据
        /// </summary>
        /// <returns></returns>
        Task<PageModel<UserQueryModel>> QueryUserPageAsync(UserQueryParam param);

        #region 修改类
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="createModel">用户提交数据</param>
        /// <returns></returns>
        Task<bool> AddAsync(UserCreateModel createModel);

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model">用户提交数据</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UserUpdateModel model);

        /// <summary>
        /// 更新用户登录信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UpdateLoginInfoAsync(string userId);

        /// <summary>
        /// 管理员更新用户密码
        /// </summary>
        /// <param name="model">用户提交信息</param>
        /// <returns></returns>
        Task<bool> ChangePwdAsync(ChangePwdModel model);

        /// <summary>
        /// 用户修改自己密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> ChangeMyPwdAsync(ChangeMyPwdModel model);

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AssignRoleAsync(AssignRoleModel model);
        #endregion

        #region 检查
        /// <summary>
        /// 检查用户名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        Task<bool> CheckUserNameAsync(string userName);

        /// <summary>
        /// 检查邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        Task<bool> CheckEmailAsync(string email);
        #endregion

    }
}
