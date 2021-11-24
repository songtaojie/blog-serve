using HxCore.Extras.SqlSugar.Repositories;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// SqlSugar 拓展类
    /// </summary>
    public static class SqlSugarServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 SqlSugar 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sqlSugarSettings"></param>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugar(this IServiceCollection services, ConnectionConfig[] sqlSugarSettings = default, Action<ISqlSugarClient> buildAction = default)
        {
            // 注册 SqlSugar 客户端
            services.AddScoped<ISqlSugarClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                if (sqlSugarSettings == null || !sqlSugarSettings.Any())
                {
                    sqlSugarSettings = config.GetSection("SqlSugarSettings").Get<ConnectionConfig[]>();
                }
                var enabledSqlLog = config.GetSection("DbSettings:EnabledSqlLog").Get<bool?>();
                var sqlSugarClient = new SqlSugarClient(sqlSugarSettings.ToList());
                if (enabledSqlLog == true)
                {
                    sqlSugarClient.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        Console.WriteLine(SqlProfiler.ParameterFormat(sql,pars));
                        Console.WriteLine();
                    };
                }
                buildAction?.Invoke(sqlSugarClient);

                return sqlSugarClient;
            });

            // 注册非泛型仓储
            services.AddScoped<ISqlSugarRepository, SqlSugarRepository>();

            // 注册 SqlSugar 仓储
            services.AddScoped(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>));

            return services;
        }
    }
}
