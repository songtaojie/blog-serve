using Hx.Sdk.Entity.Dependency;
using HxCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.Module
{
    /// <summary>
    /// 接口管理详情model
    /// </summary>
    public class ModuleDetailModel:IAutoMapper<T_Module>
    {
        /// <summary>
        /// 接口的id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 接口的地址
        /// </summary>
        public string LinkUrl { get; set; }

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
        public bool IsEnabled { get; set; }
    }
}
