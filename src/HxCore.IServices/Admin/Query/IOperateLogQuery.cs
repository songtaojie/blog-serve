using HxCore.Model.Admin.OperateLog;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 操作日志查询接口
    /// </summary>
    public interface IOperateLogQuery
    {
        /// <summary>
        /// 获取操作日志列表数据
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<OperateLogQueryModel>> QueryPageAsync(OperateLogQueryParam param);

        /// <summary>
        /// 获取用户近三十天接口访问情况
        /// </summary>
        /// <returns></returns>
        Task<OperateLogChars> GetLineDataAsync();
    }
}
