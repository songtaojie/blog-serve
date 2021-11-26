using Hx.Sdk.Entity.Page;
using HxCore.IServices.Admin;
using HxCore.Model.Admin.User;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers.Admin
{
    /// <summary>
    /// 用户管理控制器
    /// </summary>
    public class UserController : BaseAdminController
    {
        private readonly IUserService _service;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service"></param>
        public UserController(IUserService service)
        {
            _service = service;
        }

        #region 查询
        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<UserQueryModel>> GetPageAsync(UserQueryParam param)
        {
            var result = await _service.QueryUserPageAsync(param);
            return result;
        }

        /// <summary>
        /// 获取用户详情数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<UserDetailModel> GetAsync(string id)
        {
            var result = await _service.GetAsync(id);
            return result;
        }

        /// <summary>
        /// 获取用户详情数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserDetailModel> GetCurrentUserInfoAsync()
        {
            var result = await _service.GetCurrentUserInfoAsync();
            return result;
        }


        /// <summary>
        ///  获取用户所拥有的的角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{accountId}")]
        public async Task<List<UserRoleModel>> GetRoleByIdAsync(string accountId)
        {
            var result = await _service.GetRoleByIdAsync(accountId);
            return result;
        }
        #endregion

        #region 操作
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddAsync(UserCreateModel createModel)
        {
            return await _service.AddAsync(createModel);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAsync(UserUpdateModel model)
        {
            return await _service.UpdateAsync(model);
        }

        /// <summary>
        /// 编辑自己的信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateMyInfoAsync(UserUpdateModel model)
        {
            return await _service.UpdateAsync(model);
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">要删除的接口的id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(string id)
        {
            return await _service.DeleteAsync(id);
        }


        /// <summary>
        /// 管理员修改其他人密码
        /// </summary>
        /// <param name="model">用户提交的数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> ChangePwdAsync(ChangePwdModel model)
        {
            return await _service.ChangePwdAsync(model);
        }

        /// <summary>
        /// 用户修改自己的密码
        /// </summary>
        /// <param name="model">用户提交的数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> ChangeMyPwdAsync(ChangeMyPwdModel model)
        {
            return await _service.ChangeMyPwdAsync(model);
        }

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="model">用户提交的数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AssignRoleAsync(AssignRoleModel model)
        {
            return await _service.AssignRoleAsync(model);
        }
        #endregion

        #region 检查
        /// <summary>
        /// 检查用户名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        [HttpGet("{userName}")]
        public async Task<bool> CheckUserNameAsync(string userName)
        {
            return await _service.CheckUserNameAsync(userName);
        }

        /// <summary>
        /// 检查邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        [HttpGet("{email}")]
        public async Task<bool> CheckEmailAsync(string email)
        {
            return await _service.CheckEmailAsync(email);
        }
        #endregion
    }
}
