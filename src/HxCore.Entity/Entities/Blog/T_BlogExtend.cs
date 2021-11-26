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
        /// 正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 内容，html格式
        /// </summary>
        public string ContentHtml { get; set; }
    }
}
