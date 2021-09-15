using Hx.Sdk.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    public class T_OperateLog : Hx.Sdk.DatabaseAccessor.IEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public T_OperateLog()
        {
            Id = Helper.GetSnowId();
            OperateTime = DateTime.Now;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [StringLength(36)]
        public string Id { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [StringLength(200)]
        public string IPAddress { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(500)]
        public string Location { get; set; }

        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Agent
        /// </summary>
        [MaxLength(2000)]
        public string Agent { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [MaxLength(500)]
        public string Url { get; set; }

        /// <summary>
        /// 类名称
        /// </summary>
        [MaxLength(100)]
        public string ControllerName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        [MaxLength(100)]
        public string ActionName { get; set; }

        /// <summary>
        /// 请求方式（GET POST PUT DELETE)
        /// </summary>
        [MaxLength(10)]
        public string HttpMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        public long ElapsedTime { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperateTime { get; set; }

        /// <summary>
        /// 操作人id
        /// </summary>
        [MaxLength(36)]
        public string OperaterId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [MaxLength(36)]
        public string Operater { get; set; }
    }
}
