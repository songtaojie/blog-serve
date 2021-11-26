using Hx.Sdk.Common.Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 后台账户表
    /// </summary>
    public class T_Account : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(36)]
        public override string Id 
        { 
            get => base.Id; 
            set => base.Id = value; 
        }
        /// <summary>
        /// 账户
        /// </summary>
        [MaxLength(36)]
        [Required]
        public string AccountName
        {
            get; set;
        }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(36)]
        public string PassWord
        {
            set;
            get;
        }
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(36)]
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Email
        {
            set; get;
        }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string Lock { set; get; } = ConstKey.No;

        /// <summary>
        /// 头像存储文件路径
        /// </summary>
        [MaxLength(500)]
        public string AvatarUrl
        {
            get; set;
        }
       
        /// <summary>
        /// 是否是管理员
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string SuperAdmin
        {
            get; set;
        } = ConstKey.No;

        /// <summary>
        /// 用户注册时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime RegisterTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 使用MarkDown编辑器
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string UseMdEdit { get; set; } = ConstKey.No;
        /// <summary>
        /// 登录的ip
        /// </summary>
        [MaxLength(100)]
        public string LoginIp { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public virtual DateTime? LastLoginTime { get; set; }
    }
}
