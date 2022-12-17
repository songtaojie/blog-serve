using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxBlog.Entity.Entities
{
    /// <summary>
    /// 首页Banner图
    /// </summary>
    public class T_BannerInfo:Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [MaxLength(500)]
        [Required]
        public string ImgUrl { get; set; }

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

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
    }
}
