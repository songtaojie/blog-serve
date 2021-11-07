using System.ComponentModel.DataAnnotations;
using Hx.Sdk.DatabaseAccessor;
namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 博客扩展表
    /// </summary>
    public class T_BlogExtend: Hx.Sdk.DatabaseAccessor.Base.EntityPropertyBase, IEntity
    {
        /// <summary>
        /// 主键，这里直接和T_Blog的主键保持一致
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 博客id
        /// </summary>
        [MaxLength(36)]
        public string BlogId { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 内容，html格式
        /// </summary>
        public string ContentHtml { get; set; }

        /// <summary>
        /// 转发或者翻译的原链接
        /// </summary>
        [MaxLength(255)]
        public string ForwardUrl { get; set; }
       
        /// <summary>
        /// 发改主题时的坐标
        /// </summary>
        [MaxLength(255)]
        public string Location { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [MaxLength(50)]
        public string City { get; set; }
        
    }
}
