using DotNetCore.CAP;
using HxCore.EventBus;
using HxCore.EventBus.RabbitMQ;
using HxCore.EventBus.RabbitMQ.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RabbitMQServiceCollectionExtensions
    {
        /// <summary>
        /// 添加RabbitMq
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mqOption">配置</param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, Func<RabbitMqSettings> mqOption)
        {
            if (mqOption == null) throw new ArgumentNullException(nameof(mqOption), "no configuration information passed in");
            var option = mqOption.Invoke();
            //注册IRabbitMQPersistentConnection服务用于设置RabbitMQ连接
            services.AddSingleton<IRabbitMQPersistentConnection>(resolver =>
            {
                var logger = resolver.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = option.HostName,
                    DispatchConsumersAsync = true,
                    VirtualHost = option.VirtualHost,
                    UserName = option.UserName,
                    Password = option.Password,
                    Port = option.Port,
                };
                return new DefaultRabbitMQPersistentConnection(factory, logger, option.RetryCount);
            });

            //注册单例模式的EventBusRabbitMQ
            services.AddSingleton<IEventBus, EventBusRabbitMQ>();
            return services;
        }

        /// <summary>
        /// 添加rabbitmq，配置文件中需要配置RabbitMqSettings节点
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            //注册IRabbitMQPersistentConnection服务用于设置RabbitMQ连接
            services.AddSingleton<IRabbitMQPersistentConnection>(resolver =>
            {
                var logger = resolver.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var configuration = resolver.GetRequiredService<IConfiguration>();
                var section = configuration.GetSection("RabbitMqSettings");
                if(section == null) throw new MissingMemberException("RabbitMqSettings is missing from the configuration file");
                var option = section.Get<RabbitMqSettings>();
                var factory = new ConnectionFactory()
                {
                    HostName = option.HostName,
                    DispatchConsumersAsync = true,
                    VirtualHost = option.VirtualHost,
                    UserName = option.UserName,
                    Password = option.Password,
                    Port = option.Port,
                };
                return new DefaultRabbitMQPersistentConnection(factory, logger, option.RetryCount);
            });

            //注册单例模式的EventBusRabbitMQ
            services.AddSingleton<IEventBus, EventBusRabbitMQ>();
            return services;
        }


        public static IServiceCollection AddCapRabbitMq(this IServiceCollection services, Action<CapOptions> capOptions,
           Action<RabbitMQOptions> mqOption)
        {
            if (mqOption == null)
                throw new ArgumentNullException(nameof(mqOption), "调用 RabbitMQ 配置时出错，未传入配置信息。");

            services.AddCap(options =>
            {
                capOptions?.Invoke(options);
                options.UseRabbitMQ(mqOption);
            });

            services.AddTransient<IEventBus, EventBusCapRabbitMQ>();
            return services;
        }
    }
}
