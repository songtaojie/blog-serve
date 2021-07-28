using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HxCore.Extensions.Document
{
    /// <summary>
    /// 控制器注释文档model
    /// </summary>
    public class DocumentModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string ActionId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ActionFullName { get; set; }

        /// <summary>
        /// action类型
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// 路由模板
        /// </summary>
        public string RouteTemplate { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string HttpMethod { get; set; }

    }
}
