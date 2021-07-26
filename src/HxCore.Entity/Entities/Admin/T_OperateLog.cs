using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    public class T_OperateLog : Hx.Sdk.DatabaseAccessor.EntityBase
    {
        /// <summary>
        /// 请求的路由地址
        /// </summary>
        [StringLength(200)]
        public string Route { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [StringLength(200)]
        public string IPAddress { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(2000)]
        public string Description { get; set; }
    }
}
