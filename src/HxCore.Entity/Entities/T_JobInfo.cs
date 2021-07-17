using System.ComponentModel.DataAnnotations;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 工作信息
    /// </summary>
    public class T_JobInfo : Hx.Sdk.DatabaseAccessor.EntityBase
    {
        /// <summary>
        /// 职位
        /// </summary>
        [MaxLength(100)]
        public string Position
        {
            get; set;
        }
        /// <summary>
        /// 行业
        /// </summary>
        [MaxLength(100)]
        public string Industry
        {
            get; set;
        }
        /// <summary>
        /// 工作单位
        /// </summary>
        [MaxLength(100)]
        public string WorkUnit
        {
            get; set;
        }
        /// <summary>
        /// 工作年限
        /// </summary>
        public int? WorkYear
        {
            get; set;
        }

        /// <summary>
        /// 目前状态
        /// </summary>
        [MaxLength(20)]
        public string Status
        {
            get; set;
        }
        /// <summary>
        /// 熟悉的技术
        /// </summary>
        [MaxLength(1000)]
        public string Skills
        {
            get; set;
        }
        /// <summary>
        /// 擅长的领域
        /// </summary>
        [MaxLength(1000)]
        public string GoodAreas
        {
            get; set;
        }

        /// <summary>
        /// 用户的id
        /// </summary>
        [MaxLength(36)]
        public string UserId { get; set; }
    }
}
