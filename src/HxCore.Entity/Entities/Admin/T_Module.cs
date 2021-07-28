using System.ComponentModel.DataAnnotations;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 接口API地址信息表
    /// </summary>
    public class T_Module : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 接口的全名称
        /// </summary>
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 接口的地址
        /// </summary>
        [MaxLength(100)]
        public string RouteUrl { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        [MaxLength(100)]
        public string Area { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        [MaxLength(100)]
        public string Controller { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        [MaxLength(100)]
        public string Action { get; set; }

        /// <summary>
        /// 该接口的请求方式
        /// </summary>
        [MaxLength(50)]
        public string HttpMethod { get; set; }

        /// <summary>
        /// /描述
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
    }
}
