using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.Menu
{
    /// <summary>
    /// 下拉菜单model
    /// </summary>
    public class MenuPullDownModel: BaseTreeModel<MenuPullDownModel>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
    }
}
