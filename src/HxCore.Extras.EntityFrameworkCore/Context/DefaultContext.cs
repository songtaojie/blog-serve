using Microsoft.EntityFrameworkCore;
using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.EntityConfiguration;

namespace HxCore.Extras.EntityFrameworkCore
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    [AppDbContext("MySqlConnectionString", DbProvider.MySqlOfficial)]
    public class DefaultContext : AppDbContext<DefaultContext>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">配置</param>
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new UserInfoEtc());
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
