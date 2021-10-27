using Consul;
using Hx.Sdk.ConfigureOptions;
using HxCore.Entity.Options;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class ConsulAppBuilderExtensions
    {
        /// <summary>
        /// 服务注册到consul
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsulService(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            var consulSettings = AppSettings.GetConfig<ConsulSettings>("ConsulSettings");
            if (consulSettings==null || string.IsNullOrEmpty(consulSettings.Address)) throw new Exception("ConsulSettings configuration missing");
            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(consulSettings.Address);
            });
            if(consulSettings.AgentService ==null) throw new Exception("ConsulSettings:AgentService configuration missing");
            var agentService = new AgentServiceRegistration
            {
                ID = Guid.NewGuid().ToString(),
                Name = consulSettings.AgentService.Name,
                Address = consulSettings.AgentService.Address,
                Port = consulSettings.AgentService.Port,
                Tags = consulSettings.AgentService.Tags,
                Check = new Consul.AgentServiceCheck
                {
                    //ID = Guid.NewGuid().ToString(),
                    //Name = consulSettings.AgentCheck?.Name,
                    HTTP = consulSettings.AgentCheck?.HTTP,
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(consulSettings.AgentCheck.DeregisterCriticalServiceAfter??5),
                    Interval = TimeSpan.FromSeconds(consulSettings.AgentCheck.Interval??10)
                }
            };
            //服务注册
            consulClient.Agent.ServiceRegister(agentService);
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(agentService.ID).Wait();
            });
            return app;
        }
    }
}
