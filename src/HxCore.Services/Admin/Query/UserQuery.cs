using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.User;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class UserQuery : BaseQuery<T_Account>, IUserQuery
    {
        public UserQuery(ISqlSugarRepository<T_Account> repository):base(repository)
        { 
            
        }
        public async Task<UserDetailModel> GetAsync(string id)
        {
            var userDetail = await this.Repository.Entities.Where(u => u.Id == id)
              .Select(u => new UserDetailModel
              { 
                  Id = u.Id,
                  AvatarUrl = u.AvatarUrl,
                  Email = u.Email,
                  Lock = u.Lock,
                  UserName = u.AccountName,
                  NickName = u.NickName,
                  UseMdEdit = u.UseMdEdit
              })
              .FirstAsync();
            if (userDetail == null) throw new UserFriendlyException("用户信息不存在", ErrorCodeEnum.DataNull);
            return userDetail;
        }

        public async Task<UserDetailModel> GetCurrentUserInfoAsync()
        {
            return await GetAsync(UserContext.UserId);
        }

        public async Task<List<UserRoleModel>> GetRoleByIdAsync(string accountId)
        {
            var roles = await this.Db.Queryable<T_Role, T_AccountRole>((r, ur) => new JoinQueryInfos(JoinType.Inner, r.Id == ur.RoleId))
                .Where((r, ur) => ur.AccountId == accountId && r.Deleted == ConstKey.No)
                .Select((r, ur) => new UserRoleModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                })
                .ToListAsync();
            return roles;
        }

        public async Task<SqlSugarPageModel<UserQueryModel>> QueryUserPageAsync(UserQueryParam param)
        {
            var result = await this.Repository.Entities.Where(u => u.Deleted == ConstKey.No)
                .Select(u => new UserQueryModel
                {
                    Id = u.Id,
                    UserName = u.AccountName,
                    AvatarUrl = u.AvatarUrl,
                    Email = u.Email,
                    NickName = u.NickName,
                    LastLoginTime = u.LastLoginTime,
                    Lock = u.Lock,
                    LoginIp = u.LoginIp,
                    RegisterTime = u.RegisterTime,
                })
                .ToPagedListAsync(param.PageIndex,param.PageSize);
            //获取角色信息
            if (result.Items.Any())
            {
                var userIds = result.Items.Select(u => u.Id).ToArray();

                var roles = await this.Db.Queryable<T_Role, T_AccountRole>((r, ur) => new JoinQueryInfos(JoinType.Inner, r.Id == ur.RoleId))
                          .Where((r, ur) => userIds.Contains(ur.AccountId))
                          .Select((r, ur) => new
                          {
                              ur.AccountId,
                              ur.RoleId,
                              RoleName = r.Name
                          })
                          .ToListAsync();
                foreach (var u in result.Items)
                {
                    var roleNames = roles.Where(r => r.AccountId == u.Id).Select(r => r.RoleName);
                    if (roleNames.Any()) u.UserRoleName = string.Join(",", roleNames);
                }
            }
            return result;
        }
    }
}
