using System.ComponentModel.DataAnnotations;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 博客标签
    /// </summary>
    public class T_BlogTag : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 标签名字
        /// </summary>
        [MaxLength(36)]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? OrderIndex { get; set; }
    }
}
