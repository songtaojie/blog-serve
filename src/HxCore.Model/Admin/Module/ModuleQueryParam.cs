using Hx.Sdk.Entity.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.Module
{
    /// <summary>
    /// 接口查询参数
    /// </summary>
    public class ModuleQueryParam:BasePageParam
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name { get; set; }
    }
}
