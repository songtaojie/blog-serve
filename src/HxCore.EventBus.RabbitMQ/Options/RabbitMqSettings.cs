using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.EventBus.RabbitMQ.Options
{
    public class RabbitMqSettings
    {
        /// <summary>
        /// 获取或设置主机的名称。
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 获取或设置端口。
        /// </summary>
        public int Port { get; set; } = 15672;
        /// <summary>
        /// 获取或设置用户的名称。
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 获取或设置密码。
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 获取或设置虚拟主机。
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; } = 5;
    }
}
