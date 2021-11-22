using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Entity
{
    /// <summary>
    /// 缓存的键
    /// </summary>
    public class CacheKeyConfig
    {
        /// <summary>
        /// token
        /// </summary>
        public const string AuthTokenKey = ":AUTH:USER_TOKEN_{0}";

        /// <summary>
        /// Permission
        /// </summary>
        public const string AuthRouterKey = ":AUTH:USER_PERMISSION_{0}";

        /// <summary>
        /// Permission
        /// </summary>
        public const string BlogKey = ":Blog";

        /// <summary>
        /// 系统内置数据
        /// </summary>
        public const string SystemMenuKey = ":SystemMenu";
    }
}
