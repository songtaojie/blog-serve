using Hx.Sdk.DatabaseAccessor;
using HxCore.Extras.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFCoreServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 数据访问 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseAccessor(this IServiceCollection services)
        {
            services.AddDatabaseAccessor(service =>
            {
                service.AddDbPool<DefaultContext>();
                service.AddDbPool<IdsDbContext, IdsDbContextLocator>();
            });
            return services;
        }
    }
}
