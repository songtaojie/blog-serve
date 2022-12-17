using System.ComponentModel.DataAnnotations;

namespace HxBlog.Entity.Entities
{
    /// <summary>
    /// 博客分类
    /// </summary>
    public class T_Category : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 分类名字
        /// </summary>
        [MaxLength(40)]
        public string Name { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
    }
}
