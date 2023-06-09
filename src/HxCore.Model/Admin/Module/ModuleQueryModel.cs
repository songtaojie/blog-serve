﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.Module
{
    /// <summary>
    /// 接口查询model
    /// </summary>
    public class ModuleQueryModel
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
        /// /描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
       
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creater { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
