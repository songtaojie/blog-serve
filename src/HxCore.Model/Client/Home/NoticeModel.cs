using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Client
{
    /// <summary>
    /// 公告通知model
    /// </summary>
    public  class NoticeModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 跳转方式
        /// </summary>
        public string Target { get; set; } = "_blank";
    }
}
