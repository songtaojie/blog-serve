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
using HxCore.Model.Admin.Menu;
using HxCore.Model.Admin.Module;

namespace HxCore.Services.Admin
{
    public class PermissionService : BaseStatusService<T_Menu>, IPermissionService
    {
        private readonly IRedisCache _redisCache;
        private readonly IUserService _userService;
        public PermissionService(IRepository<T_Menu, MasterDbContextLocator> userDal, IRedisCache redisCache,
            IUserService userService) : base(userDal)
        {
            _redisCache = redisCache;
            _userService = userService;
        }

        #region 新增编辑
        /// <inheritdoc cref="IPermissionService.AddAsync(MenuCreateModel)"/>
        public async Task<bool> AddAsync(MenuCreateModel createModel)
        {
            var entity = this.Mapper.Map<T_Menu>(createModel);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserContext.UserId, UserContext.UserName);
            this.BeforeInsert(entity);
            if (entity.MenuType == T_Menu_Enum.Button) entity.Path = string.Empty;
            //再添加
            if (createModel.ModuleIds != null && createModel.ModuleIds.Any())
            {
                var addModules = createModel.ModuleIds.Select(m => new T_MenuModule
                {
                    Id = Helper.GetSnowId(),
                    PermissionId = entity.Id,
                    ModuleId = m
                });
                await this.Db.Set<T_MenuModule>().AddRangeAsync(addModules);
            }
            await Repository.InsertAsync(entity);
            var result = await Repository.SaveNowAsync();
            return result > 0;
        }

        /// <inheritdoc cref="IPermissionService.UpdateAsync(MenuUpdateModel)"/>
        public async Task<bool> UpdateAsync(MenuUpdateModel model)
        {
            if (string.IsNullOrEmpty(model.Id)) throw new Hx.Sdk.FriendlyException.UserFriendlyException("无效的标识", ErrorCodeEnum.UpdateError);
            var entity = await this.FindAsync(model.Id);
            if (entity == null) throw new Hx.Sdk.FriendlyException.UserFriendlyException("未找到角色信息", ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(model, entity);
            var disabled = model.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserContext.UserId, UserContext.UserName);
            if (entity.MenuType == T_Menu_Enum.Button) entity.Path = string.Empty;
            //先删除原来关联的接口
            var removeList = this.Db.Set<T_MenuModule>().Where(r => r.PermissionId == entity.Id).ToList();
            if (removeList.Any())
            {
                this.Db.Set<T_MenuModule>().RemoveRange(removeList);
            }
            //再添加
            if (model.ModuleIds != null && model.ModuleIds.Any())
            {
                var addModules = model.ModuleIds.Select(m => new T_MenuModule
                {
                    Id = Helper.GetSnowId(),
                    PermissionId = entity.Id,
                    ModuleId = m
                });
                await this.Db.Set<T_MenuModule>().AddRangeAsync(addModules);
            }
            return await this.UpdateAsync(entity);
        }
        #endregion

        #region 查询
        /// <inheritdoc cref="IPermissionService.GetListAsync()"/>
        public async Task<List<MenuQueryModel>> GetListAsync()
        {
            List<MenuQueryModel> menuList = null;
            var isSuperAdmin = await _userService.CheckIsSuperAdminAsync(UserContext.UserId);
            if (isSuperAdmin)
            {
                var query = from m in this.Repository.DetachedEntities
                            join mm in this.Db.Set<T_MenuModule>().AsNoTracking() on m.Id equals mm.PermissionId into mm_temp
                            from mm in mm_temp.DefaultIfEmpty()
                            join md in this.Db.Set<T_Module>().AsNoTracking() on mm.ModuleId equals md.Id into md_temp
                            from md in md_temp.DefaultIfEmpty()
                            where m.Deleted == ConstKey.No
                            orderby m.OrderSort, m.CreateTime descending
                            select this.Mapper.Map(m, new MenuQueryModel
                            {
                                IsEnabled = m.Disabled == ConstKey.No,
                                RouteUrl = md.RouteUrl
                            });
                menuList = await query.ToListAsync();
            }
            else
            {
                var roleIds = UserContext.GetClaimValueByType("roleId").ToArray();
                var query = from m in this.Repository.DetachedEntities
                            join rm in this.Db.Set<T_RoleMenu>().AsNoTracking() on m.Id equals rm.PermissionId
                            join mm in this.Db.Set<T_MenuModule>().AsNoTracking() on m.Id equals mm.PermissionId into mm_temp
                            from mm in mm_temp.DefaultIfEmpty()
                            join md in this.Db.Set<T_Module>().AsNoTracking() on mm.ModuleId equals md.Id into md_temp
                            from md in md_temp.DefaultIfEmpty()
                            where m.Deleted == ConstKey.No
                            && roleIds.Contains(rm.RoleId)
                            orderby m.OrderSort descending
                            select this.Mapper.Map(m, new MenuQueryModel
                            {
                                IsEnabled = m.Disabled == ConstKey.No,
                                RouteUrl = md.RouteUrl
                            });
                menuList = await query.ToListAsync();
            }
            //处理一个菜单多个接口的数据，把多个接口合并到一个数据上
            menuList = menuList.GroupBy(m => m.Id).Select(m =>
            {
                var model = m.First();
                model.RouteUrl = string.Join(",", m.Select(r => r.RouteUrl));
                return model;
            }).ToList();
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
            bool isSuperAdmin = await _userService.CheckIsSuperAdminAsync(UserContext.UserId);
            if (isSuperAdmin)
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
        public async Task<UserRouterModel> GetRoutersAsync()
        {
            var cacheData = await GetUserPermissionAsync();
            var result = new UserRouterModel();
            if (cacheData != null)
            {
                List<RouterModel> routerList = cacheData.Routers.Where(m => m.MenuType != T_Menu_Enum.Button)
                    .GroupBy(m => m.Id).Select(m => m.First()).ToList();
                result.Routers = RecursionHelper.HandleTreeChildren(routerList);
                result.Buttons = cacheData.Routers.Where(m => m.MenuType == T_Menu_Enum.Button).Select(m => m.Code).Distinct().ToList();
            }
            return result;
        }

        /// <summary>
        /// 获取用户权限数据
        /// </summary>
        /// <returns></returns>
        public async Task<UserPermissionCached> GetUserPermissionAsync()
        {
            var cacheKey = string.Format(CacheKeyConfig.AuthRouterKey, UserContext.UserId);
            UserPermissionCached cacheData = await _redisCache.GetAsync<UserPermissionCached>(cacheKey);
            if (cacheData != null) return cacheData;

            var isSuperAdmin = await _userService.CheckIsSuperAdminAsync(UserContext.UserId);
            List<RouterModel> menuList;
            var roleIds = new List<string>();
            if (isSuperAdmin)
            {
                menuList = await GetAllMenu();
            }
            else
            {
                (menuList, roleIds) = await GetUserMenu();
            }

            //缓存数据
            cacheData = new UserPermissionCached();
            if (menuList.Any())
            {
                cacheData.IsSuperAdmin = isSuperAdmin;
                cacheData.RoleIds = roleIds;
                cacheData.Routers = menuList;
                cacheData.Modules = menuList.Select(m => m.Module).ToList();
                await _redisCache.SetAsync(cacheKey, cacheData, TimeSpan.FromMinutes(10));
            }
            return cacheData;
        }

        /// <summary>
        /// 获取所有的菜单
        /// </summary>
        /// <returns></returns>
        private async Task<List<RouterModel>> GetAllMenu()
        {
            var permissionTrees = await (from m in this.Repository.DetachedEntities
                                         join pm in this.Db.Set<T_MenuModule>() on m.Id equals pm.PermissionId into pm_temp
                                         from pm in pm_temp.DefaultIfEmpty()
                                         join md in this.Db.Set<T_Module>() on pm.ModuleId equals md.Id into md_temp
                                         from md in md_temp.DefaultIfEmpty()
                                         where m.Deleted == ConstKey.No
                                         orderby m.OrderSort
                                         select new RouterModel
                                         {
                                             Id = m.Id,
                                             Code = m.Code,
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
                                             },
                                             Module = new RouterModuleModel
                                             {
                                                 PermissionId = m.Id,
                                                 Name = md.Name,
                                                 RouteUrl = md.RouteUrl,
                                                 HttpMethod = md.HttpMethod
                                             }
                                         }).ToListAsync();
            return permissionTrees;
        }

        /// <summary>
        /// 获取某个用户的菜单
        /// </summary>
        /// <returns></returns>
        private async Task<(List<RouterModel>, List<string>)> GetUserMenu()
        {
            List<RouterModel> permissionTrees = new List<RouterModel>();
            var roleIds = UserContext.GetClaimValueByType("roleId");
            if (roleIds.Any())
            {
                var menuIds = await this.Db.Set<T_RoleMenu>()
                    .Where(rm => roleIds.Contains(rm.RoleId))
                    .Select(rm => rm.PermissionId)
                    .ToArrayAsync();
                if (menuIds.Any())
                {
                    permissionTrees = (from m in this.Repository.DetachedEntities
                                       join mm in this.Db.Set<T_MenuModule>() on m.Id equals mm.PermissionId into mm_temp
                                       from mm in mm_temp.DefaultIfEmpty()
                                       join md in this.Db.Set<T_Module>() on mm.ModuleId equals md.Id into md_temp
                                       from md in md_temp.DefaultIfEmpty()
                                       where m.Deleted == ConstKey.No
                                           && menuIds.Contains(m.Id)
                                       orderby m.OrderSort
                                       select new RouterModel
                                       {
                                           Id = m.Id,
                                           Code = m.Code,
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
                                               PermissionId = m.Id,
                                               Name = md.Name,
                                               RouteUrl = md.RouteUrl,
                                               HttpMethod = md.HttpMethod
                                           }
                                       }).ToList();

                }
            }
            return (permissionTrees, roleIds);
        }

        /// <inheritdoc cref="IPermissionService.GetAsync"/>
        public async Task<MenuDetailModel> GetAsync(string id)
        {
            var entity = await this.FindAsync(id);
            if (entity == null) throw new Hx.Sdk.FriendlyException.UserFriendlyException("未找到菜单信息", ErrorCodeEnum.DataNull);
            var detailModel = this.Mapper.Map(entity, new MenuDetailModel
            {
                IsEnabled = Helper.IsNo(entity.Disabled),
            });
            //获取接口数据
            detailModel.ModuleList = await (from pm in this.Db.Set<T_MenuModule>()
                                            join m in this.Db.Set<T_Module>() on new { pm.ModuleId, Disabled = ConstKey.No } equals new { ModuleId = m.Id, m.Disabled }
                                            where pm.PermissionId == entity.Id
                                            select this.Mapper.Map(m, new ModuleDetailModel
                                            {
                                                IsEnabled = Helper.IsNo(m.Disabled)
                                            })).ToListAsync();
            detailModel.ModuleIds = detailModel.ModuleList.Select(m => m.Id).ToList();
            return detailModel;
        }
        #endregion
    }
}
