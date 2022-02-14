using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Admin.FriendLink
{
    /// <summary>
    /// 友情链接基础model
    /// </summary>
    public class FriendLinkManageBaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 网站代码code
        /// </summary>
        public string SiteCode { get; set; }

        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 网站链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 网站logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
    }
}
