using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class T_UserRole : Hx.Sdk.DatabaseAccessor.IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [MaxLength(36)]
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [MaxLength(36)]
        [Required]
        public string RoleId { get; set; }
    }
}
