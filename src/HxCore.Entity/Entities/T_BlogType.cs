using System.ComponentModel.DataAnnotations;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 博客的类型
    /// </summary>
    public class T_BlogType : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 名字
        /// </summary>
        [MaxLength(40)]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? OrderIndex { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
