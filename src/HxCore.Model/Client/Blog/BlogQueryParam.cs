using Hx.Sdk.Entity.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Client
{
    /// <summary>
    /// 博客查询参数
    /// </summary>
    public class BlogQueryParam: BasePageParam
    {
        /// <summary>
        /// 标签id
        /// </summary>
        public string TagId { get; set; }
    }
}
