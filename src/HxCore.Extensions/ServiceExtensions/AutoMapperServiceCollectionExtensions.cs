using Hx.Sdk.Core;
using HxCore.Extensions.AutoMapper;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 添加AutoMapper
    /// </summary>
    public static class AutoMapperServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Automapper映射
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddAutoMapper(c =>
            {
                c.AddProfile(new MyMapperProfile(App.Assemblies));
                //c.CreateMap<Entity.Entities.T_Blog, Model.Admin.Blog.BlogManageQueryModel>()
                //.ForMember(dest => dest.Id, opt => { opt.MapFrom(b => b.Id.ToString()); });
            });
            return services;
        }
    }
}
