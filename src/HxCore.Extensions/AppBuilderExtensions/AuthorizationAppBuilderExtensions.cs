using Consul;
using Hx.Sdk.ConfigureOptions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class AuthorizationAppBuilderExtensions
    {
        /// <summary>
        /// 服务注册到consul
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsulService(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {

            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(AppSettings.GetConfig("ConsulSettings:ConsulAddress"));
            });
            var agentService = AppSettings.GetConfig<AgentServiceRegistration>("ConsulSettings:AgentService");
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
