using System.ComponentModel.DataAnnotations;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class T_Role : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 创建角色
        /// </summary>
        public T_Role()
        {
        }
        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="createrId">创建人id</param>
        /// <param name="creater">创建人姓名</param>
        public T_Role(string name, string createrId, string creater)
        {
            Name = name;
            Description = string.Empty;
            SetCreater(createrId, creater);
        }
       
        /// <summary>
        /// 角色名
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 角色Code
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        ///描述
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        ///排序
        /// </summary>
        public int OrderSort { get; set; }
    }
}
