namespace HxCore.Options
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

        /// <summary>
        /// 健康检查配置
        /// </summary>
        public AgentServiceCheck AgentCheck { get; set; }
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

    /// <summary>
    /// 代理服务的健康检查
    /// </summary>
    public class AgentServiceCheck
    {
        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string HTTP { get; set; }
        /// <summary>
        /// 健康检查时间间隔
        /// </summary>
        public int? Interval { get; set; }
        /// <summary>
        /// 服务启动多久后注册
        /// </summary>
        public int? DeregisterCriticalServiceAfter { get; set; }
        /// <summary>
        /// 健康检查名称
        /// </summary>
        public string Name { get; set; }
    }
}
