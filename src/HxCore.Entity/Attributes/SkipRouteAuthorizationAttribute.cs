using System;

namespace Hx.Sdk.Attributes
{
    /// <summary>
    /// 跳过授权策略
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SkipRouteAuthorizationAttribute : Attribute
    {
    }
}
