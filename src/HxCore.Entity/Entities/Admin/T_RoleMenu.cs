using System.ComponentModel.DataAnnotations;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 角色权限按钮表
    /// </summary>
    public class T_RoleMenu : Hx.Sdk.DatabaseAccessor.IEntity
    {
        /// <summary>
        /// 角色权限表
        /// </summary>
        public T_RoleMenu()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [MaxLength(36)]
        [Required]
        public string RoleId { get; set; }

        /// <summary>
        ///  菜单ID
        /// </summary>
        [MaxLength(36)]
        [Required]
        public string PermissionId { get; set; }
    }
}
