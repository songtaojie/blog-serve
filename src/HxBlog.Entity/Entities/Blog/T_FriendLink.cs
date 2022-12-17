using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxBlog.Entity.Entities
{
    /// <summary>
    /// 友情链接
    /// </summary>
    public class T_FriendLink: Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 网站代码code
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string SiteCode { get; set; }

        /// <summary>
        /// 网站名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string SiteName { get; set; }
        /// <summary>
        /// 网站链接
        /// </summary>
        [MaxLength(500)]
        public string Link { get; set; }
        /// <summary>
        /// 网站logo
        /// </summary>
        [MaxLength(200)]
        public string Logo { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
    }
}
