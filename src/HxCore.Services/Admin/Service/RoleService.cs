using Hx.Sdk.Common.Helper;
using Hx.Sdk.Entity.Page;
using HxCore.Entity.Entities;
using HxCore.Model.Admin.Role;
using System.Linq;
using System.Threading.Tasks;
using HxCore.Entity;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.Extensions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Hx.Sdk.FriendlyException;
using HxCore.Enums;
using HxCore.IServices;

namespace HxCore.Services
{

    public class RoleService : BaseStatusService<T_Role>, IRoleService
    {
        public RoleService(IRepository<T_Role,MasterDbContextLocator> userDal) : base(userDal)
        {
        }

        #region 新增编辑
        /// <inheritdoc cref="IRoleService.AddAsync"/>
        public async Task<bool> AddAsync(RoleCreateModel createModel)
        {
            var entity = this.Mapper.Map<T_Role>(createModel);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserContext.UserId, UserContext.UserName);
            return await this.InsertAsync(entity);
        }

        /// <inheritdoc cref="IRoleService.UpdateAsync"/>
        public async Task<bool> UpdateAsync(RoleCreateModel createModel)
        {
            if (string.IsNullOrEmpty(createModel.Id)) throw new UserFriendlyException("无效的标识",ErrorCodeEnum.ParamsNullError);
            var entity = await this.FindAsync(createModel.Id);
            if (entity == null) throw new UserFriendlyException("未找到角色信息",ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(createModel, entity);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserContext.UserId, UserContext.UserName);
            return await this.UpdateAsync(entity);
        }

        /// <inheritdoc cref="IRoleService.AssignPermission"/>
        public async Task<bool> AssignPermission(AssignPermissionModel model)
        {
            model.VerifyParam();
            //先删除原来的菜单
            var roleMenuRep = this.Repository.Change<T_RoleMenu>();
            var removeEntitys = await roleMenuRep.Where(rm => rm.RoleId == model.RoleId).ToListAsync();
            if(removeEntitys.Any()) await roleMenuRep.DeleteAsync(removeEntitys);

            //添加菜单
            List<T_RoleMenu> menuList = model.MenuIds.Select(m => new T_RoleMenu
            {
                RoleId = model.RoleId,
                PermissionId = m
            }).ToList();
            await roleMenuRep.InsertAsync(menuList);
            return await roleMenuRep.SaveNowAsync() > 0;
        }
        #endregion

        #region 查询
        /// <inheritdoc cref="IRoleService.QueryRolePageAsync"/>
        public Task<PageModel<RoleQueryModel>> QueryRolePageAsync(RoleQueryParam param)
        {
            var query = from r in this.Repository.DetachedEntities
                        where r.Deleted == ConstKey.No
                        orderby r.OrderSort descending,r.CreateTime descending
                        select new RoleQueryModel
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Code = r.Code,
                            Description = r.Description,
                            OrderSort = r.OrderSort,
                            CreateTime = r.CreateTime,
                            Creater = r.Creater,
                            IsEnabled = r.Disabled == ConstKey.No
                        };
            return query.ToOrderAndPageListAsync(param);
        }

        /// <inheritdoc cref="IRoleService.GetAsync"/>
        public async Task<RoleDetailModel> GetAsync(string id)
        {
            var detail = await this.FindAsync(id);
            if (detail == null) throw new UserFriendlyException("未找到角色信息",ErrorCodeEnum.DataNull);
            RoleDetailModel detailModel = this.Mapper.Map<RoleDetailModel>(detail);
            detailModel.IsEnabled = Helper.IsNo(detail.Disabled);
            return detailModel;
        }


        /// <inheritdoc cref="IRoleService.GetListByUserAsync"/>
        public async Task<List<RoleQueryModel>> GetListByUserAsync(string accountId)
        {
            var query = from r in this.Repository.DetachedEntities
                        join ru in this.Repository.Context.Set<T_AccountRole>() on r.Id equals ru.RoleId into ru_temp
                        from ru in ru_temp.DefaultIfEmpty()
                        where ru.AccountId == accountId && r.Deleted == ConstKey.No 
                        select new RoleQueryModel
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Code = r.Code,
                            Description = r.Description,
                            OrderSort = r.OrderSort,
                            CreateTime = r.CreateTime,
                            Creater = r.Creater,
                            IsEnabled = r.Disabled == ConstKey.No
                        };
            return await query.ToListAsync();
        }

        /// <inheritdoc cref="IRoleService.GetSelectMenuListAsync(string)"/>
        public async Task<List<string>> GetSelectMenuListAsync(string roleId)
        {
            var checkMenus = await (from m in this.Db.Set<T_Menu>().AsNoTracking()
                        join rm in Db.Set<T_RoleMenu>().AsNoTracking() on m.Id equals rm.RoleId
                        where rm.RoleId == roleId
                        && m.Deleted == ConstKey.No
                        select new
                        {
                            m.Id,
                            m.ParentId,
                            m.OrderSort
                        }).ToListAsync();
            //排除父级菜单
            var parentIds = checkMenus.Where(m => !string.IsNullOrEmpty(m.ParentId)).Select(m => m.ParentId).Distinct();
            var menuIds = checkMenus.Where(m => !parentIds.Contains(m.Id))
                .OrderBy(m => m.OrderSort)
                .Select(m => m.Id)
                .ToList();

            return menuIds;
        }
        /// <inheritdoc cref="IRoleService.GetListAsync"/>
        public async Task<List<RoleQueryModel>> GetListAsync()
        {
            return await this.Repository.DetachedEntities.Where(r => r.Deleted == ConstKey.No && r.Disabled == ConstKey.No)
                 .Select(r => new RoleQueryModel
                 {
                     Id = r.Id,
                     Code = r.Code,
                     Name = r.Name,
                 })
                 .ToListAsync();
        }

        /// <inheritdoc cref="IRoleService.GetAllRoleMenusAsync"/>
        public async Task<List<RoleMenuModel>> GetAllRoleMenusAsync()
        {
            var query = from r in this.Repository.DetachedEntities
                        join rm in this.Db.Set<T_RoleMenu>() on r.Id equals rm.RoleId
                        join m in this.Db.Set<T_Menu>() on rm.PermissionId equals m.Id
                        where r.Deleted == ConstKey.No
                        && m.Deleted == ConstKey.No
                        select new RoleMenuModel
                        {
                            RoleId = r.Id,
                            MenuId = rm.PermissionId
                        };
            return await query.ToListAsync();
        }

        /// <inheritdoc cref="IRoleService.CheckIsSuperAdminAsync"/>
        public async Task<bool> CheckIsSuperAdminAsync(string accountId)
        {
            var query = from r in this.Repository.DetachedEntities
                        join ur in Db.Set<T_AccountRole>() on r.Id equals ur.RoleId
                        where r.Deleted == ConstKey.No
                        && ur.AccountId == accountId
                        && r.Code == ConstKey.SuperAdminCode
                        select r.Id;
            return await query.AnyAsync();
        }

        /// <inheritdoc cref="IRoleService.GetUserRoleAsync"/>
        public async Task<List<Model.Admin.User.UserRoleModel>> GetUserRoleAsync(string accountId)
        {
            var query = from r in this.Repository.DetachedEntities
                        join ur in this.Db.Set<T_AccountRole>().AsNoTracking() on r.Id equals ur.RoleId
                        where r.Deleted == ConstKey.No
                        && ur.AccountId == accountId
                        select new Model.Admin.User.UserRoleModel
                        {
                            RoleId = r.Id,
                            RoleName = r.Name
                        };
            return await query.ToListAsync();
        }

        /// <inheritdoc cref="IRoleService.GetPermissionsAsync"/>
        public async Task<List<string>> GetPermissionsAsync(string id)
        {
            return await this.Db.Set<T_RoleMenu>()
                .Where(rm => rm.RoleId == id)
                .Select(rm => rm.PermissionId)
                .ToListAsync(); 
        }
        #endregion

    }
}
