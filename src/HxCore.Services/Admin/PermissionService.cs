using Hx.Sdk.Entity;
using Hx.Sdk.Entity.Page;
using HxCore.Entity.Entities;
using HxCore.IServices;
using HxCore.Model.Admin;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HxCore.Entity;
using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.Extensions;
using System.Collections.Generic;
using Hx.Sdk.Cache;
using System;
using HxCore.Entity.Enum;
using HxCore.Model;
using HxCore.IServices.Admin;
using Hx.Sdk.ConfigureOptions;
using HxCore.IServices.Ids4;

namespace HxCore.Services.Admin
{
    public class PermissionService : BaseStatusService<T_Menu>, IPermissionService
    {
        private readonly IRedisCache _redisCache;
        private readonly IRoleService _roleService;
        private readonly IIds4RoleService _ids4RoleService;
        public PermissionService(IRepository<T_Menu, MasterDbContextLocator> userDal, IRedisCache redisCache,
            IRoleService roleService, IIds4RoleService ids4RoleService) : base(userDal)
        {
            _redisCache = redisCache;
            _roleService = roleService;
            _ids4RoleService = ids4RoleService;
        }

        #region 新增编辑
        /// <inheritdoc cref="HxCore.IServices.IPermissionService.AddAsync(MenuCreateModel)"/>
        public async Task<bool> AddAsync(MenuCreateModel createModel)
        {
            var entity = this.Mapper.Map<T_Menu>(createModel);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserContext.UserId, UserContext.UserName);
            if (entity.MenuType == T_Menu_Enum.Button) entity.Path = string.Empty;

            return await this.InsertAsync(entity);
        }

        /// <inheritdoc cref="HxCore.IServices.IPermissionService.InsertAsync(MenuCreateModel)"/>
        public async Task<bool> UpdateAsync(MenuCreateModel createModel)
        {
            if (string.IsNullOrEmpty(createModel.Id)) throw new Hx.Sdk.FriendlyException.UserFriendlyException("无效的标识", ErrorCodeEnum.UpdateError);
            var entity = await this.FindAsync(createModel.Id);
            if (entity == null) throw new Hx.Sdk.FriendlyException.UserFriendlyException("未找到角色信息", ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(createModel, entity);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserContext.UserId, UserContext.UserName);
            if (entity.MenuType == T_Menu_Enum.Button) entity.Path = string.Empty;
            return await this.UpdateAsync(entity);
        }
        #endregion

        #region 查询
        /// <inheritdoc cref="IPermissionService.GetListAsync()"/>
        public async Task<List<MenuQueryModel>> GetListAsync()
        {
            List<MenuQueryModel> menuList = null;

            if (UserContext.IsSuperAdmin)
            {
                var query = from m in this.Repository.DetachedEntities
                            where m.Deleted == ConstKey.No
                            orderby m.OrderSort, m.CreateTime descending
                            select this.Mapper.Map(m, new MenuQueryModel
                            {
                                IsEnabled = m.Disabled == ConstKey.No
                            });
                menuList = await query.ToListAsync();
            }
            else
            {
                var roleIds = UserContext.GetClaimValueByType("roleId").ToArray();
                var query = from m in this.Repository.DetachedEntities
                            join rm in this.Db.Set<T_RoleMenu>().AsNoTracking() on m.Id equals rm.PermissionId
                            where m.Deleted == ConstKey.No
                            && roleIds.Contains(rm.RoleId)
                            orderby m.OrderSort descending
                            select this.Mapper.Map(m, new MenuQueryModel
                            {
                                IsEnabled = m.Disabled == ConstKey.No
                            });
                menuList = await query.ToListAsync();
            }
            return RecursionHelper.HandleTreeChildren(menuList);
        }


        /// <summary>
        /// 获取当前用户的路由
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc cref="IPermissionService.GetUserMenuTreeAsync()"/>
        public async Task<List<MenuPullDownModel>> GetUserMenuTreeAsync()
        {
            List<MenuPullDownModel> menuList = new List<MenuPullDownModel>();
            var cacheKey = string.Format(CacheKeyConfig.AuthRouterKey, UserContext.UserId);
            menuList = await _redisCache.GetAsync<List<MenuPullDownModel>>(cacheKey);
            if (menuList != null && menuList.Any()) return menuList;
            if (UserContext.IsSuperAdmin)
            {
                menuList = await (from m in this.Repository.DetachedEntities
                                  where m.Deleted == ConstKey.No
                                  orderby m.OrderSort
                                  select new MenuPullDownModel
                                  {
                                      Id = m.Id,
                                      Name = m.Name,
                                      ParentId = m.ParentId,
                                      SortNumber = m.OrderSort
                                  }).ToListAsync();
            }
            else
            {
                var roleIds = UserContext.GetClaimValueByType("roleId").Distinct().ToList();
                if (roleIds.Any())
                {
                    var menuQuery = from m in this.Repository.DetachedEntities
                                    join rm in this.Db.Set<T_RoleMenu>().AsNoTracking() on m.Id equals rm.PermissionId
                                    where m.Deleted == ConstKey.No
                                    && roleIds.Contains(rm.RoleId)
                                    orderby m.OrderSort descending
                                    select new MenuPullDownModel
                                    {
                                        Id = m.Id,
                                        Name = m.Name,
                                        ParentId = m.ParentId,
                                        SortNumber = m.OrderSort,
                                    };
                    menuList = await menuQuery.ToListAsync();
                }
            }
            menuList = RecursionHelper.HandleTreeChildren(menuList);
            return menuList;
        }


        /// <summary>
        /// 获取当前用户的路由
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc cref="IPermissionService.GetRoutersAsync()"/>
        public async Task<List<RouterQueryModel>> GetRoutersAsync()
        {
            List<RouterQueryModel> menuList;
            var cacheKey = string.Format(CacheKeyConfig.AuthRouterKey, UserContext.UserId);
            menuList = await _redisCache.GetAsync<List<RouterQueryModel>>(cacheKey);
            if (menuList != null && menuList.Any()) return menuList;
            var isSuperAdmin = await CheckIsSuperAdminAsync(UserContext.UserId);
            if (isSuperAdmin)
            {
                menuList = await GetAllMenu();
            }
            else
            {
                menuList = await GetUserMenu();
            }
            menuList = RecursionHelper.HandleTreeChildren(menuList);
            if (menuList.Any())
            {
                await _redisCache.SetAsync(cacheKey, menuList, TimeSpan.FromMinutes(5));
            }
            return menuList;
        }
        /// <summary>
        /// 获取所有的菜单
        /// </summary>
        /// <returns></returns>
        private async Task<List<RouterQueryModel>> GetAllMenu()
        {
            var permissionTrees = await (from m in this.Repository.DetachedEntities
                                         join pm in this.Db.Set<T_MenuModule>() on m.Id equals pm.PermissionId into pm_temp
                                         from pm in pm_temp.DefaultIfEmpty()
                                         join md in this.Db.Set<T_Module>() on pm.ModuleId equals md.Id into md_temp
                                         from md in md_temp.DefaultIfEmpty()
                                         where m.Deleted == ConstKey.No
                                         orderby m.OrderSort
                                         select new RouterQueryModel
                                         {
                                             Id = m.Id,
                                             Name = m.Name,
                                             ParentId = m.ParentId,
                                             Path = m.Path,
                                             Component = m.Component,
                                             OrderSort = m.OrderSort,
                                             Hidden = m.IsHide,
                                             MenuType = m.MenuType,
                                             Meta = new RouterMetaModel
                                             {
                                                 Title = m.Name,
                                                 IskeepAlive = m.IskeepAlive,
                                                 Icon = m.Icon
                                             }
                                         }).ToListAsync();
            return permissionTrees;
        }

        /// <summary>
        /// 获取某个用户的菜单
        /// </summary>
        /// <returns></returns>
        private async Task<List<RouterQueryModel>> GetUserMenu()
        {
            List<RouterQueryModel> permissionTrees = new List<RouterQueryModel>();
            var roleIds = UserContext.GetClaimValueByType("roleId");
            if (roleIds.Any())
            {
                var menuIds = await this.Db.Set<T_RoleMenu>().Where(rm => roleIds.Contains(rm.RoleId)).Select(rm => rm.PermissionId).ToArrayAsync();
                if (menuIds.Any())
                {
                    permissionTrees = (from m in this.Repository.DetachedEntities
                                       join pm in this.Db.Set<T_MenuModule>() on m.Id equals pm.PermissionId into pm_temp
                                       from pm in pm_temp.DefaultIfEmpty()
                                       join md in this.Db.Set<T_Module>() on pm.ModuleId equals md.Id into md_temp
                                       from md in md_temp.DefaultIfEmpty()
                                       where m.Deleted == ConstKey.No
                                           && menuIds.Contains(m.Id)
                                       orderby m.OrderSort
                                       select new RouterQueryModel
                                       {
                                           Id = m.Id,
                                           Name = m.Name,
                                           Path = m.Path,
                                           ParentId = m.ParentId,
                                           Component = m.Component,
                                           OrderSort = m.OrderSort,
                                           Hidden = m.IsHide,
                                           MenuType = m.MenuType,
                                           Meta = new RouterMetaModel
                                           {
                                               Title = m.Name,
                                               IskeepAlive = m.IskeepAlive,
                                               Icon = m.Icon
                                           },
                                           Module = new RouterModuleModel
                                           {
                                               Name = md.Name,
                                               RouteUrl = md.RouteUrl,
                                               HttpMethod = md.HttpMethod
                                           }
                                       }).ToList();
                    //var query = from r in this.Db.Set<T_Role>()
                    //    from m in this.Repository.DetachedEntities
                    //            where m.Deleted == ConstKey.No
                    //            && m.MenuType != T_Menu_Enum.Button
                    //            && menuIds.Contains(m.Id)
                    //            orderby m.OrderSort
                    //            select new RouterQueryModel
                    //            {
                    //                Id = m.Id,
                    //                Name = m.Name,
                    //                Path = m.Path,
                    //                ParentId = m.ParentId,
                    //                Component = m.Component,
                    //                OrderSort = m.OrderSort,
                    //                Hidden = m.IsHide,
                    //                MenuType = m.MenuType,
                    //                Meta = new RouterQueryMetaModel
                    //                {
                    //                    Title = m.Name,
                    //                    IskeepAlive = m.IskeepAlive,
                    //                    Icon = m.Icon
                    //                }
                    //            }


                }
            }
            return permissionTrees;
        }

        /// <inheritdoc cref="IPermissionService.GetAsync"/>
        public async Task<MenuDetailModel> GetAsync(string id)
        {
            var detail = await this.FindAsync(id);
            if (detail == null) throw new Hx.Sdk.FriendlyException.UserFriendlyException("未找到角色信息", ErrorCodeEnum.DataNull);
            MenuDetailModel detailModel = this.Mapper.Map<MenuDetailModel>(detail);
            detailModel.IsEnabled = Helper.IsNo(detail.Disabled);
            return detailModel;
        }

        private async Task<bool> CheckIsSuperAdminAsync(string userId)
        {
            if (App.Settings.UseIdentityServer4 ?? false)
            {
                return await _ids4RoleService.CheckIsSuperAdminAsync(userId);
            }
            return await _roleService.CheckIsSuperAdminAsync(userId);
        }
        #endregion
    }
}
