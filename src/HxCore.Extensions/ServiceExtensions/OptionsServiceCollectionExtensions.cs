using Hx.Sdk.Attributes;
using HxCore.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Options服务拓展类
    /// </summary>
    [SkipScan]
    public static class OptionsServiceCollectionExtensions
    {
        /// <summary>
        /// 配置跨域
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddCustomOptions(this IServiceCollection services)
        {
            services.AddConfigureOptions<AttachSettingsOptions>();
            services.AddConfigureOptions<ElasticSettingsOptions>();
            return services;
        }
    }
}
