using HxCore.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using Hx.Sdk.Extensions;
using Hx.Sdk.Entity.Dependency;
using HxCore.Entity.Entities;
using Newtonsoft.Json;

namespace HxCore.Model.Admin.Menu
{
    /// <summary>
    /// 菜单按权限查询数据model
    /// </summary>
    public class MenuQueryModel:BaseTreeModel<MenuQueryModel>,IAutoMapper<T_Menu>
    {
        /// <summary>
        /// 权限的code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///菜单类型；0：目录，1：菜单，2：按钮
        /// </summary>
        public T_Menu_Enum MenuType { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// /描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// 是否是隐藏菜单
        /// </summary>
        public bool IsHide { get; set; }

        /// <summary>
        /// 是否keepAlive
        /// </summary>
        public bool IskeepAlive { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 请求的接口
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        ///菜单类型；0：目录，1：菜单，2：按钮
        /// </summary>
        public string Type_V => MenuType.GetDescription();
    }
}
