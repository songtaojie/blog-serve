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
        public const string AuthTokenKey = ":Auth:Token_User_{0}";

        /// <summary>
        /// Permission
        /// </summary>
        public const string AuthRouterKey = ":Auth:Router_User_{0}";
    }
}
