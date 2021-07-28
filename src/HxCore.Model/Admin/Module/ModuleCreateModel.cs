using Hx.Sdk.Entity.Dependency;
using HxCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HxCore.Model.Admin.Module
{
    /// <summary>
    /// 创建/更新接口所用的model
    /// </summary>
    public class ModuleCreateModel:IAutoMapper<T_Module>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 接口的地址,即路由地址
        /// </summary>
        public string RouteUrl { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 该接口的请求方式
        /// </summary>
        public string HttpMethod { get; set; }
        /// <summary>
        /// /描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 接口的详细信息
        /// </summary>
        public MethodInfo MethodInfo { get; set; }
    }
}
