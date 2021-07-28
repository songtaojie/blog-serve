using Hx.Sdk.Common.Extensions;
using Hx.Sdk.Common.Helper;
using HxCore.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HxCore.Entity.Data
{
    /// <summary>
    /// 种子
    /// </summary>
    public class SeedData
    {
        //初始化用户
        private static readonly string UserId = Guid.NewGuid().ToString();
        private static readonly string UserName = "SuperAdmin";
        /// <summary>
        /// 执行迁移文件并初始化数据
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            //迁移，在解决方案文件路径中执行迁移，指定启动项目路径为HxCore.Web，迁移文件项目为HxCore.Entity
            //dotnet ef -s src/HxCore.Web  -p  src/HxCore.Entity  migrations add InitTable  -c DefaultContext
            //dotnet ef -s src/HxCore.Web  -p  src/HxCore.Entity migrations remove  -c DefaultContext
            //dotnet run -p src/HxCore.Web /seed
            ConsoleHelper.WriteSuccessLine("Seeding database...");
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context.DefaultContext>();
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
            var userInfoDb = context.Set<T_User>();
            if (!userInfoDb.Any())
            {
                ConsoleHelper.WriteSuccessLine("T_User being populated");
                var user = new T_User()
                {
                    Id = UserId,
                    UserName = UserName,
                    SuperAdmin = ConstKey.Yes,
                    PassWord = "123456".MD5TwoEncrypt(),
                    Activate = ConstKey.Yes,
                    Email = "stjworkemail@163.com",
                    NickName = "超级管理员",
                    LastLoginTime = DateTime.Now
                };
                user.SetCreater(UserId, UserName);
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
                    Code = "SuperAdmin",
                    Name = "超级管理员",
                    Description = "超级管理员权限",
                    Deleted = ConstKey.No,
                    Disabled = ConstKey.No,
                    OrderSort = 0,
                };
                role.SetCreater(UserId, UserName);
                roleDb.Add(role);
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("T_Role already populated");
            }
            //用户角色表
            var userRoleDb = context.Set<T_UserRole>();
            if (!userRoleDb.Any())
            {
                ConsoleHelper.WriteSuccessLine("T_UserRole being populated");
                var userRole = new T_UserRole
                {
                    Id = Helper.GetSnowId(),
                    UserId = UserId,
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
            var blogTypeDb = context.Set<T_BlogType>();
            if (!blogTypeDb.Any())
            {
                ConsoleHelper.WriteSuccessLine("T_BlogType being populated");
                foreach (var blogType in GetBlogTypeList())
                {
                    blogType.SetCreater(UserId, UserName);
                    blogTypeDb.Add(blogType);
                }
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("T_BlogType already populated");
            }

            var categoryDb = context.Set<T_Category>();
            if (!categoryDb.Any())
            {
                ConsoleHelper.WriteSuccessLine("T_Category being populated");
                foreach (var category in GetCategoryList())
                {
                    category.SetCreater(UserId, UserName);
                    categoryDb.Add(category);
                }
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("T_Category already populated");
            }
        }


        private static List<T_BlogType> GetBlogTypeList()
        {
            return new List<T_BlogType>
            {
               new T_BlogType
                {
                    Id = Helper.GetSnowId(),
                    Name="原创",
                    OrderIndex = 0
                },
                new T_BlogType
                {
                    Id = Helper.GetSnowId(),
                    Name="转载",
                    OrderIndex = 1
                },
                new T_BlogType
                {
                    Id = Helper.GetSnowId(),
                    Name="翻译",
                    OrderIndex = 2
                }
            };
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
                var menu = new T_Menu
                {
                    Id = Helper.GetSnowId(),
                    Path = "/permission",
                    Component = "/permission/index.vue",
                    Name = "菜单管理",
                    MenuType = Enum.T_Menu_Enum.Catalog,
                    Icon = "el-icon-menu",
                    Description = "菜单",
                    OrderSort = 0
                };
                menu.SetCreater(UserId, UserName);
                menuDb.Add(menu);
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
