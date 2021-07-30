using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin
{
    /// <summary>
    /// 路由查询model
    /// </summary>
    public class RouterQueryModel: BaseTreeModel<RouterQueryModel>
    {

        /// <summary>
        /// 页面的name
        /// </summary>
        public string Name { get; set; } 

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 组件地址
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        ///菜单类型；0：目录，1：菜单，2：按钮
        /// </summary>
        public Entity.Enum.T_Menu_Enum MenuType { get; set; }

        /// <summary>
        /// 是否隐藏路由，当设置 true 的时候该路由不会再侧边栏出现
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// Meta数据
        /// </summary>
        public RouterMetaModel Meta { get; set; }

        /// <summary>
        /// 接口信息
        /// </summary>
        [JsonIgnore]
        public RouterModuleModel Module { get; set; }
    }
    /// <summary>
    /// Meta数据model
    /// </summary>
    public class RouterMetaModel
    {
        /// <summary>
        /// 是否需要权限
        /// </summary>
        public bool RequireAuth { get; set; } = true;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 是否keepAlive
        /// </summary>
        public bool IskeepAlive { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 不显示左侧菜单栏
        /// </summary>
        public bool NoTab { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RouterModuleModel
    {
        /// <summary>
        /// 接口的全名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 接口的地址
        /// </summary>
        public string RouteUrl { get; set; }
        /// <summary>
        /// 该接口的请求方式
        /// </summary>
        public string HttpMethod { get; set; }
    }
}
