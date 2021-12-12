using Hx.Sdk.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 博客标签映射表
    /// </summary>
    public class T_BlogTag : IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 博客主键
        /// </summary>
        [MaxLength(36)]
        public string BlogId { get; set; }

        /// <summary>
        /// 标签主键
        /// </summary>
        [MaxLength(36)]
        public string TagId { get; set; }
    }
}
