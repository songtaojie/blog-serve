using System;

namespace Hx.Sdk.Attributes
{
    /// <summary>
    /// 跳过日志记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SkipOperateLogAttribute : Attribute
    {
    }
}
