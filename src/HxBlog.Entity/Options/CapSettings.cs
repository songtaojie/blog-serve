namespace HxCore.Options
{
    /// <summary>
    ///  Cap配置
    /// </summary>
    public class CapSettings
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 默认组
        /// </summary>
        public string DefaultGroup { get; set; }

        /// <summary>
        /// RabbitMQ配置
        /// </summary>
        public RabbitMQSettings RabbitMQ { get; set; }

    }

    /// <summary>
    /// RabbitMQ配置
    /// </summary>
    public class RabbitMQSettings
    {
        /// <summary>
        /// 主机名称
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 虚拟主机名称
        /// </summary>
        public string VirtualHost { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
    }
}
