using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HxCore.Options
{
    /// <summary>
    /// DbSettings配置
    /// </summary>
    public class DbSettingsOptions : Hx.Sdk.ConfigureOptions.IConfigurableOptions<DbSettingsOptions>
    {
        /// <summary>
        /// db配置
        /// </summary>
        public List<DbSettings> DbSettings { get; set; }

        /// <summary>
        /// 第一个配置
        /// </summary>
        public DbSettings FirstDbSetting { get; set; }
        /// <summary>
        /// 后置配置
        /// </summary>
        /// <param name="options">选项</param>
        /// <param name="configuration">配置</param>
        public void PostConfigure(DbSettingsOptions options, IConfiguration configuration)
        {
            if (DbSettings == null) throw new Exception("未在appSettings.json中找到DbSettings节点");
            if (DbSettings.Count == 0) throw new Exception("至少设置一个可用的数据库连接");
            DbSettings = DbSettings.Where(d => d.Enabled).ToList();
            FirstDbSetting = DbSettings.FirstOrDefault();
        }
    }
}
