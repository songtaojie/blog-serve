using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Entity.Options
{
    /// <summary>
    /// Consul配置
    /// </summary>
    public class ConsulSettings
    {
        /// <summary>
        /// Consul地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 代理服务
        /// </summary>
        public AgentService AgentService { get; set; }
    }
    /// <summary>
    /// 代理服务配置
    /// </summary>
    public class AgentService
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// tag标签，以便Fabio识别
        /// </summary>
        public string[] Tags { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
}
