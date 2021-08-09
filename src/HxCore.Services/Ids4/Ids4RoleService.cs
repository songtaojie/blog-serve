using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Context;
using HxCore.Entity.Entities;
using HxCore.IServices.Ids4;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using HxCore.Entity;
using Microsoft.EntityFrameworkCore;
using HxCore.Model.Admin.User;
using HxCore.Entity.Entities.Ids4;
using Hx.Sdk.DependencyInjection;
using HxCore.Model.Admin.Role;
using Hx.Sdk.Entity.Page;
using Hx.Sdk.Extensions;

namespace HxCore.Services.Ids4
{
    /// <summary>
    /// IDS4角色管理
    /// </summary>
    public class Ids4RoleService:Internal.PrivateService<ApplicationRole, IdsDbContextLocator>, IIds4RoleService, IScopedDependency
    {
        public Ids4RoleService(IRepository<ApplicationRole, IdsDbContextLocator> roleDal) : base(roleDal)
        {
        }

        #region 查询
        /// <inheritdoc cref="IIds4RoleService.QueryRolePageAsync"/>
        public Task<PageModel<RoleQueryModel>> QueryRolePageAsync(RoleQueryParam param)
        {
            var query = from r in this.Repository.DetachedEntities
                        where r.Deleted == ConstKey.No
                        orderby r.OrderSort descending, r.CreateTime descending
                        select new RoleQueryModel
                        {
                            Id = r.Id,
                            Name = r.Description,
                            Code = r.Name,
                            Description = r.Description,
                            OrderSort = r.OrderSort,
                            CreateTime = r.CreateTime,
                            Creater = r.Creater,
                            IsEnabled = r.Enabled == ConstKey.Yes
                        };
            return query.ToOrderAndPageListAsync(param);
        }
        #endregion


        /// <inheritdoc cref="IIds4RoleService.CheckIsSuperAdminAsync"/>
        public async Task<bool> CheckIsSuperAdminAsync(string userId)
        {
            var query = from r in this.Repository.DetachedEntities
                        join ur in Db.Set<ApplicationUserRole>().AsNoTracking() on r.Id equals ur.RoleId
                        where r.Deleted == ConstKey.No
                        && ur.UserId == userId
                        && r.Name == ConstKey.SuperAdminCode
                        select r.Id;
            return await query.AnyAsync();
        }

       

        /// <inheritdoc cref="IIds4RoleService.GetUserRoleAsync"/>
        public async Task<List<UserRoleModel>> GetUserRoleAsync(string userId)
        {
            var query = from r in this.Repository.DetachedEntities
                        join ur in this.Db.Set<ApplicationUserRole>().AsNoTracking() on r.Id equals ur.RoleId
                        where r.Deleted == ConstKey.No
                        && ur.UserId == userId
                        select new Model.Admin.User.UserRoleModel
                        {
                            RoleId = r.Id,
                            RoleName = r.Name
                        };
            return await query.ToListAsync();
        }

       
    }
}
