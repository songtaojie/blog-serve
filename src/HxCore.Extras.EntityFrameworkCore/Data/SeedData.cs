using Hx.Sdk.Common.Extensions;
using Hx.Sdk.Common.Helper;
using HxCore.Entity;
using HxCore.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using HxCore.Enums;

namespace HxCore.Extras.EntityFrameworkCore
{
    /// <summary>
    /// 种子
    /// </summary>
    public class SeedData
    {
        //初始化用户
        private static readonly string AccountId = Guid.NewGuid().ToString();
        private static readonly string AccountName = "SuperAdmin";
        /// <summary>
        /// 执行迁移文件并初始化数据
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            //迁移，在解决方案文件路径中执行迁移，指定启动项目路径为HxCore.Web，迁移文件项目为HxCore.Entity
            //dotnet ef -s src/HxCore.WebApi  -p  src/HxCore.Extras.EntityFrameworkCore  migrations add InitTable  -c DefaultContext
            //dotnet ef -s src/HxCore.WebApi  -p  src/HxCore.Extras.EntityFrameworkCore migrations remove  -c DefaultContext
            //dotnet run -p src/HxCore.WebApi /seed
            ConsoleHelper.WriteSuccessLine("Seeding database...");
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Extras.EntityFrameworkCore.DefaultContext>();
                ConsoleHelper.WriteInfoLine($"数据库连接：{context.Database.GetDbConnection().ConnectionString}");
                context.Database.Migrate();
                EnsureAllSeedData(context);
            }
            ConsoleHelper.WriteSuccessLine("Done seeding database.");
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="context"></param>
        public static void EnsureSeedData(DbContext context)
        {
            ConsoleHelper.WriteSuccessLine("Seeding database...");
            ConsoleHelper.WriteInfoLine($"数据库连接：{context.Database.GetDbConnection().ConnectionString}");
            EnsureAllSeedData(context);
            ConsoleHelper.WriteSuccessLine("Done seeding database.");
        }

        /// <summary>
        /// 客户端授权数据
        /// </summary>
        /// <param name="context"></param>
        private static void EnsureAllSeedData(DbContext context)
        {
            var userInfoDb = context.Set<T_Account>();
            if (!userInfoDb.Any())
            {
                ConsoleHelper.WriteSuccessLine("T_User being populated");
                var user = new T_Account()
                {
                    Id = AccountId,
                    AccountName = AccountName,
                    SuperAdmin = ConstKey.Yes,
                    PassWord = "123456".MD5TwoEncrypt(),
                    Email = "stjworkemail@163.com",
                    NickName = "超级管理员",
                    LastLoginTime = DateTime.Now
                };
                user.SetCreater(AccountId, AccountName);
                userInfoDb.Add(user);
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("T_UserInfo already populated");
            }
            EnsureRoleSeedData(context);
            EnsureEnumSeedData(context);
            EnsureMenuSeedData(context);
        }

        /// <summary>
        /// 添加角色种子数据
        /// </summary>
        /// <param name="context"></param>
        private static void EnsureRoleSeedData(DbContext context)
        {
            //角色表
            var roleDb = context.Set<T_Role>();
            var roleId = Helper.GetSnowId();
            if (!roleDb.Any())
            {
                ConsoleHelper.WriteSuccessLine("T_Role being populated");
                var role = new T_Role
                {
                    Id = roleId,
                    Code = ConstKey.SuperAdminCode,
                    Name = "超级管理员",
                    Description = "超级管理员权限",
                    Deleted = ConstKey.No,
                    Disabled = ConstKey.No,
                    OrderSort = 0,
                };
                role.SetCreater(AccountId, AccountName);
                roleDb.Add(role);
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("T_Role already populated");
            }
            //用户角色表
            var userRoleDb = context.Set<T_AccountRole>();
            if (!userRoleDb.Any())
            {
                ConsoleHelper.WriteSuccessLine("T_UserRole being populated");
                var userRole = new T_AccountRole
                {
                    Id = Helper.GetSnowId(),
                    AccountId = AccountId,
                    RoleId = roleId
                };
                userRoleDb.Add(userRole);
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("T_UserRole already populated");
            }
        }
        #region 用户数据
        /// <summary>
        /// 添加角色种子数据
        /// </summary>
        /// <param name="context"></param>
        private static void EnsureEnumSeedData(DbContext context)
        {
            var categoryDb = context.Set<T_Category>();
            if (!categoryDb.Any())
            {
                ConsoleHelper.WriteSuccessLine("T_Category being populated");
                foreach (var category in GetCategoryList())
                {
                    category.SetCreater(AccountId, AccountName);
                    categoryDb.Add(category);
                }
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("T_Category already populated");
            }
        }


        private static List<T_Category> GetCategoryList()
        {
            return new List<T_Category>
            {
                new T_Category{
                    Name="前端",
                    Id = Helper.GetSnowId(),
                    OrderIndex = 0
                },
                new T_Category
                {
                    Name="后端",
                    Id = Helper.GetSnowId(),
                    OrderIndex = 1
                },
                new T_Category
                {
                    Name="编程语言",
                    Id = Helper.GetSnowId(),
                    OrderIndex = 2
                }
            };
        }
        #endregion

        #region 菜单数据
        /// <summary>
        /// 添加菜单种子数据
        /// </summary>
        /// <param name="context"></param>
        private static void EnsureMenuSeedData(DbContext context)
        {
            //角色表
            var menuDb = context.Set<T_Menu>();
            if (!menuDb.Any())
            {
                ConsoleHelper.WriteSuccessLine("T_Menu being populated");
                var addList = new List<T_Menu>();
                //添加菜单
                var menu = new T_Menu
                {
                    Id = Helper.GetSnowId(),
                    IsSystem = true,
                    Code= "permission",
                    Path = "/permission",
                    Component = "/permission/index.vue",
                    Name = "菜单管理",
                    MenuType = T_Menu_Enum.Menu,
                    Icon = "el-icon-menu",
                    Description = "系统内置，请勿删除",
                    OrderSort = 0
                };
                menu.SetCreater(AccountId, AccountName);
                addList.Add(menu);
                
                //添加按钮
                var addBtn = new T_Menu
                {
                    Id = Helper.GetSnowId(),
                    Code = "permission_add",
                    IsSystem = true,
                    Path = "-",
                    Component = "-",
                    Name = "添加",
                    ParentId = menu.Id,
                    MenuType = T_Menu_Enum.Button,
                    Description = "添加菜单的按钮",
                    OrderSort = 0
                };
                addList.Add(addBtn);
                //添加按钮
                var editBtn = new T_Menu
                {
                    Id = Helper.GetSnowId(),
                    Code = "permission_edit",
                    IsSystem = true,
                    Path = "-",
                    Component = "-",
                    Name = "编辑",
                    ParentId = menu.Id,
                    MenuType = T_Menu_Enum.Button,
                    Description = "编辑菜单的按钮",
                    OrderSort = 1
                };
                addList.Add(editBtn);
                //添加按钮
                var delBtn = new T_Menu
                {
                    Id = Helper.GetSnowId(),
                    Code = "permission_del",
                    IsSystem = true,
                    Path = "-",
                    Component = "-",
                    Name = "删除",
                    ParentId = menu.Id,
                    MenuType = T_Menu_Enum.Button,
                    Description = "删除菜单的按钮",
                    OrderSort = 2
                };
                addList.Add(delBtn);
                //接口管理的菜单
                var moduleMenu = new T_Menu
                {
                    Id = Helper.GetSnowId(),
                    Code = "module",
                    IsSystem = true,
                    Path = "/module",
                    Component = "/module/index.vue",
                    Name = "接口管理",
                    MenuType = T_Menu_Enum.Menu,
                    Icon = "el-icon-s-claim",
                    Description = "系统内置，请勿删除",
                    OrderSort = 1
                };
                menu.SetCreater(AccountId, AccountName);
                addList.Add(moduleMenu);
                menuDb.AddRange(addList);
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("T_Menu already populated");
            }
        }
        #endregion
    }
}
