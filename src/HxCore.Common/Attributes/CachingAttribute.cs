using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method,Inherited = true)]
    public class CachingAttribute: Attribute
    {
        /// <summary>
        /// 缓存过期时间（分钟）
        /// </summary>
        public int AbsoluteExpiration { get; set; } = 5;
    }
}
