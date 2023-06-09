﻿using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Entities;

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
