using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 时间轴
    /// </summary>
    public class T_TimeLine:Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 内容
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
        /// 跳转方式，值为_Blank/_Self/_Parent/_Top
        /// </summary>
        [MaxLength(10)]
        public string Target { get; set; }
    }
}
