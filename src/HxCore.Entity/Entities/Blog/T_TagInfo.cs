using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 博客标签
    /// </summary>
    public class T_TagInfo : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 标签名字
        /// </summary>
        [MaxLength(36)]
        public string Name { get; set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        [MaxLength(36)]
        public string BGColor { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
    }
}
