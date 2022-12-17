using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxBlog.Entity.Entities
{
    /// <summary>
    /// 公告通知
    /// </summary>
    public class T_NoticeInfo : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 通知内容
        /// </summary>
        [MaxLength(1000)]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [MaxLength(500)]
        public string Link { get; set; }

        /// <summary>
        /// 跳转方式,值为_Blank/_Self/_Parent/_Top
        /// </summary>
        [MaxLength(10)]
        public string Target { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
    }
}
