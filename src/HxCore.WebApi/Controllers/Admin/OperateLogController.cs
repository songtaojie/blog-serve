using Hx.Sdk.Attributes;
using Hx.Sdk.Entity.Page;
using HxCore.IServices;
using HxCore.Model.Admin.OperateLog;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers.Admin
{
    /// <summary>
    /// 操作日志的控制器
    /// </summary>
    [SkipOperateLog]
    public class OperateLogController : BaseAdminController
    {
        private readonly IOperateLogQuery _query;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="query"></param>
        public OperateLogController(IOperateLogQuery query)
        {
            _query = query;
        }

        /// <summary>
        /// 获取操作日志列表数据
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SqlSugarPageModel<OperateLogQueryModel>> GetPageAsync(OperateLogQueryParam param)
        {
            return await _query.QueryPageAsync(param);
        }
    }
}
