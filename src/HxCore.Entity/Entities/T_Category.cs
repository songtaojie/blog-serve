using System.ComponentModel.DataAnnotations;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 博客分类
    /// </summary>
    public class T_Category : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 分类名字
        /// </summary>
        [StringLength(40)]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? OrderIndex { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
    }
}
