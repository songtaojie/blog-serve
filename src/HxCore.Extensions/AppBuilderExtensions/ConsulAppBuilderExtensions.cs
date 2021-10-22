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
            var consulAddress = AppSettings.GetConfig("ConsulSettings:Address");
            if (string.IsNullOrEmpty(consulAddress)) throw new Exception("ConsulSettings:Address configuration missing");
            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(consulAddress);
            });
            var agentService = AppSettings.GetConfig<AgentServiceRegistration>("ConsulSettings:AgentService");
            if(agentService == null) throw new Exception("ConsulSettings:AgentService configuration missing");
            agentService.ID = Guid.NewGuid().ToString();

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
