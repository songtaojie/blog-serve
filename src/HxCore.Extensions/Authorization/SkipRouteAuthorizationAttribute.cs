using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Authorization.Infrastructure
{
    /// <summary>
    /// 跳过授权策略
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SkipRouteAuthorizationAttribute : Attribute
    {
    }
}
