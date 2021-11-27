using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Entities;
using HxCore.Model.Admin.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hx.Sdk.Extensions;
using Hx.Sdk.Common.Extensions;
using Hx.Sdk.FriendlyException;
using HxCore.Enums;
using Hx.Sdk.Entity.Page;
using HxCore.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hx.Sdk.Common.Helper;
using HxCore.IServices.Ids4;
using Hx.Sdk.Core;
using HxCore.IServices;

namespace HxCore.Services
{
    public class UserService : BaseStatusService<T_Account>, IUserService
    {
        private IRoleService _roleService;
        private IIds4RoleService _ids4RoleService;
        public UserService(IRepository<T_Account,MasterDbContextLocator> userDal, IRoleService roleService, IIds4RoleService ids4RoleService) : base(userDal)
        {
            _roleService = roleService;
            _ids4RoleService = ids4RoleService;
        }

        #region 新增编辑
        /// <inheritdoc cref="IUserService.AddAsync"/>
        public async Task<bool> AddAsync(UserCreateModel createModel)
        {
            createModel.VerifyParam();
            //var existEmail = await this.ExistAsync(u => u.Email == createModel.Email);
            //if (existEmail) throw new UserFriendlyException("邮箱已存在", ErrorCodeEnum.AddError);
            var entity = this.Mapper.Map<T_Account>(createModel);
            entity.PassWord = createModel.PassWord.MD5TwoEncrypt();
            return await this.InsertAsync(entity);
        }
        /// <inheritdoc cref="IUserService.UpdateAsync"/>
        public async Task<bool> UpdateAsync(UserUpdateModel model)
        {
            model.VerifyParam();
            var user = await this.Repository.FindAsync(model.Id);
            if (user == null) throw new UserFriendlyException("未找到用户信息", ErrorCodeEnum.DataNull);
            var entity = this.Mapper.Map(model, user);
            entity.SetModifier(UserContext.UserId, UserContext.UserName);
            return await this.UpdatePartialAsync(entity,new string[] { "NickName", "AvatarUrl", "Lock",
                "LastModifierId", "LastModifier", "LastModifyTime","UseMdEdit"});
        }

        /// <inheritdoc cref="IUserService.UpdateMyInfoAsync"/>
        public async Task<bool> UpdateMyInfoAsync(UserUpdateModel model)
        {
            model.VerifyParam();
            var user = await this.Repository.FindAsync(model.Id);
            if (user == null) throw new UserFriendlyException("未找到当前用户信息", ErrorCodeEnum.DataNull);
            var entity = this.Mapper.Map(model, user);
            entity.SetModifier(UserContext.UserId, UserContext.UserName);
            return await this.UpdatePartialAsync(entity, new string[] { "NickName","AvatarUrl", "Lock",
                "LastModifierId", "LastModifier", "LastModifyTime","UseMdEdit"});
        }

        /// <inheritdoc cref="IUserService.UpdateLoginInfoAsync"/>
        public async Task<bool> UpdateLoginInfoAsync(string userId)
        {
            var user = await this.FindAsync(userId);
            user.LoginIp = UserContext.HttpContext.GetRemoteIpAddressToIPv4();
            user.LastLoginTime = DateTime.Now;
            return await this.UpdatePartialAsync(user, "LoginIp", "LastLoginTime");
        }

        /// <inheritdoc cref="IUserService.ChangePwdAsync"/>
        public async Task<bool> ChangePwdAsync(ChangePwdModel model)
        {
            var user = await this.FindAsync(model.Id);
            if (user == null) throw new UserFriendlyException("用户不存在", ErrorCodeEnum.DataNull);
            user.PassWord = model.NewPassWord.MD5TwoEncrypt();
            return await this.UpdatePartialAsync(user, "PassWord");
        }

        /// <inheritdoc cref="IUserService.ChangeMyPwdAsync"/>
        public async Task<bool> ChangeMyPwdAsync(ChangeMyPwdModel model)
        {
            var user = await this.FindAsync(UserContext.UserId);
            if (user == null) throw new UserFriendlyException("用户不存在", ErrorCodeEnum.DataNull);
            var pwd = model.PassWord.MD5TwoEncrypt();
            if(!pwd.Equals(user.PassWord)) throw new UserFriendlyException("用户密码错误", ErrorCodeEnum.UpdateError);
            user.PassWord = model.NewPassWord.MD5TwoEncrypt();
            return await this.UpdatePartialAsync(user, "PassWord");
        }

        /// <inheritdoc cref="IUserService.AssignRoleAsync"/>
        public async Task<bool> AssignRoleAsync(AssignRoleModel model)
        {
            model.VerifyParam();
            var user = await this.Db.Set<T_Account>().FindAsync(model.AccountId);
            if (user == null) throw new UserFriendlyException("账号不存在", ErrorCodeEnum.DataNull);
            try
            {
                //先删除用户原来的角色
                var userRoleList = await this.Db.Set<T_AccountRole>()
                    .Where(ur => ur.AccountId == model.AccountId)
                    .ToListAsync();
                if (userRoleList.Any())
                {
                    this.Db.Set<T_AccountRole>().RemoveRange(userRoleList);
                }
                //再添加新的
                var addUserRoleList = model.RoleIds.Where(r => !string.IsNullOrEmpty(r))
                    .Distinct()
                    .Select(r => new T_AccountRole
                    {
                        Id = Helper.GetSnowId(),
                        AccountId = model.AccountId,
                        RoleId = r
                    });
                await this.Db.Set<T_AccountRole>().AddRangeAsync(addUserRoleList);
                return await this.Repository.SaveNowAsync() > 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 查询

        /// <inheritdoc cref="HxCore.IServices.Admin.IUserService.GetAsync"/>
        public async Task<UserDetailModel> GetAsync(string id)
        {
            var userDetail = await this.Repository.Where(u => u.Id == id)
                .Select(u => this.Mapper.Map<T_Account, UserDetailModel>(u))
                .FirstOrDefaultAsync();
            if (userDetail == null) throw new UserFriendlyException("用户信息不存在", ErrorCodeEnum.DataNull);
            return userDetail;
        }

        /// <summary>
        /// 获取当前用户的详情数据
        /// </summary>
        /// <returns></returns>
        public async Task<UserDetailModel> GetCurrentUserInfoAsync()
        {
            return await GetAsync(UserContext.UserId);
        }

        /// <inheritdoc cref="HxCore.IServices.Admin.IUserService.QueryUserPageAsync"/>
        public async Task<PageModel<UserQueryModel>> QueryUserPageAsync(UserQueryParam param)
        {
            var query = from u in this.Repository.DetachedEntities
                        where u.Deleted == ConstKey.No
                        select this.Mapper.Map<UserQueryModel>(u);
            var result = await query.ToPageListAsync(param);
            //获取角色信息
            if (result.Items.Any())
            {
                var userIds = result.Items.Select(u => u.Id).ToArray();
                var roles = await (from r in this.Db.Set<T_Role>()
                                   join ur in this.Db.Set<T_AccountRole>() on r.Id equals ur.RoleId
                                   where userIds.Contains(ur.AccountId)
                                   select new
                                   {
                                       ur.AccountId,
                                       ur.RoleId,
                                       RoleName = r.Name
                                   }).ToListAsync();
                result.Items.ForEach(u =>
                {
                    var roleNames = roles.Where(r => r.AccountId == u.Id).Select(r=>r.RoleName);
                    if(roleNames.Any()) u.UserRoleName = string.Join(",", roleNames);
                });
            }
            return result;

        }
        /// <inheritdoc cref="HxCore.IServices.Admin.IUserService.GetRoleByIdAsync"/>
        public async Task<List<UserRoleModel>> GetRoleByIdAsync(string accountId)
        {
            var roles = await(from r in this.Db.Set<T_Role>()
                              join ur in this.Db.Set<T_AccountRole>() on r.Id equals ur.RoleId
                              where ur.AccountId == accountId
                              && r.Deleted == ConstKey.No
                              select new UserRoleModel
                              {
                                  RoleId = r.Id,
                                  RoleName = r.Name
                              }).ToListAsync();
            return roles;
        }
        #endregion

        #region 检测

        /// <summary>
        /// 检查是否是SuperAdmin，这里直接用接口检查而不是使用声明中获取的，相对安全些
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> CheckIsSuperAdminAsync(string userId)
        {
            if (App.Settings.UseIdentityServer4 ?? false)
            {
                return await _ids4RoleService.CheckIsSuperAdminAsync(userId);
            }
            return await _roleService.CheckIsSuperAdminAsync(userId);
        }

        /// <inheritdoc cref="HxCore.IServices.Admin.IUserService.CheckUserNameAsync"/>
        public async Task<bool> CheckUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName)) 
                throw new UserFriendlyException("请输入账户名", ErrorCodeEnum.ParamsInValidError)
                    .SetUnifyResultStatusCode(ErrorCodeEnum.SystemError.GetHashCode());
            var user = await this.Repository.FirstOrDefaultAsync(u => u.AccountName == userName);
            if (user != null) 
                throw new UserFriendlyException("已存在该账户名", ErrorCodeEnum.SystemError)
                    .SetUnifyResultStatusCode(ErrorCodeEnum.SystemError.GetHashCode());
            return true;
        }
        /// <inheritdoc cref="HxCore.IServices.Admin.IUserService.CheckEmailAsync"/>
        public async Task<bool> CheckEmailAsync(string email)
        {
            if(string.IsNullOrEmpty(email)) 
                throw new UserFriendlyException("请输入邮箱", ErrorCodeEnum.ParamsInValidError)
                    .SetUnifyResultStatusCode(ErrorCodeEnum.SystemError.GetHashCode());
            var user = await this.Repository.FirstOrDefaultAsync(u => u.Email == email);
            if(user!=null) 
                throw new UserFriendlyException("已存在该邮箱", ErrorCodeEnum.SystemError)
                    .SetUnifyResultStatusCode(ErrorCodeEnum.SystemError.GetHashCode());
            return true;
        }
        #endregion

        /// <summary>
        /// 重写插入的虚方法
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public override T_Account BeforeInsert(T_Account entity)
        {
            if (entity != null)
            {
                entity.Id = Guid.NewGuid().ToString();
                if (UserContext != null && UserContext.IsAuthenticated)
                {
                    entity.SetCreater(UserContext.UserId, UserContext.UserName);
                }
            }
            return entity;
        }

      
    }
}
