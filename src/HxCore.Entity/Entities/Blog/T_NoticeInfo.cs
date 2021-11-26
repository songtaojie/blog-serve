using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 公告通知
    /// </summary>
    public class T_NoticeInfo : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 通知内容
        /// </summary>
        [MaxLength(2000)]
        public string Content { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [MaxLength(500)]
        public string Link { get; set; }

        /// <summary>
        /// 跳转方式
        /// </summary>
        [MaxLength(10)]
        public string Target { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? OrderIndex { get; set; }
    }
}
