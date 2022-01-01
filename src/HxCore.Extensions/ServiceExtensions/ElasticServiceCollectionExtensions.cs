using Hx.Sdk.Core;
using HxCore.IServices.Elastic;
using HxCore.Services.Elastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ElasticSearch扩展类
    /// </summary>
    public static class ElasticServiceCollectionExtensions
    {
        /// <summary>
        /// 添加ElasticSearch
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddElasticSearch(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<IElasticClientProvider, ElasticClientProvider>();
            return services;
        }
    }
}
