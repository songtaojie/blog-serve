using Hx.Sdk.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace HxCore.Extras.EntityFrameworkCore
{
    /// <summary>
    /// IdentityServer4库数据库上下文
    /// </summary>
    [AppDbContext("IdsConnectionString", DbProvider.MySqlOfficial)]
    public class IdsDbContext : AppDbContext<IdsDbContext, IdsDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public IdsDbContext(DbContextOptions<IdsDbContext> options) : base(options)
        {
        }
    }
}
