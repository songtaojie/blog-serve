using System.ComponentModel.DataAnnotations;

namespace HxCore.Entity
{
    /// <summary>
    /// 菜单与接口关系表
    /// </summary>
    public class T_MenuModule : Hx.Sdk.DatabaseAccessor.IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 接口ID
        /// </summary>
        [MaxLength(36)]
        [Required]
        public string ModuleId { get; set; }
        /// <summary>
        /// 菜单/按钮ID
        /// </summary>
        [MaxLength(36)]
        [Required]
        public string PermissionId { get; set; }
      
    }
}
