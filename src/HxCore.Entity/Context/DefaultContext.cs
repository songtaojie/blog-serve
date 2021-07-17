using Microsoft.EntityFrameworkCore;
using Hx.Sdk.DatabaseAccessor;

namespace HxCore.Entity.Context
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
    }
}
