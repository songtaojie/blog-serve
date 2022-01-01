using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Options
{
    /// <summary>
    /// elastic配置文件
    /// </summary>
    public class ElasticSettingsOptions : IPostConfigureOptions<ElasticSettingsOptions>
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// elasticsearch url集合
        /// </summary>
        public List<string> Urls { get; set; }

        /// <summary>
        /// 后置配置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void PostConfigure(string name, ElasticSettingsOptions options)
        {
            options.Enabled ??= false;
            options.Urls ??= new List<string>();
        }
    }
}
