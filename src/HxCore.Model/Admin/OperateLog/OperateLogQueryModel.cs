using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Entities;
using System;

namespace HxCore.Model.Admin.OperateLog
{
    /// <summary>
    /// 操作记录查询model
    /// </summary>
    public class OperateLogQueryModel:IAutoMapper<T_OperateLog>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求方式（GET POST PUT DELETE)
        /// </summary>
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
        /// 
        /// </summary>
        public string Agent { get; set; }

        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        public long ElapsedTime { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperateTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operater { get; set; }
    }
}
