using Hx.Sdk.Attributes;
using Hx.Sdk.Entity.Page;
using HxCore.IServices.Admin;
using HxCore.Model.Admin.OperateLog;
using HxCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Web.Controllers.Admin
{
    /// <summary>
    /// 操作日志的控制器
    /// </summary>
    [SkipOperateLog]
    public class OperateLogController : BaseAdminController
    {
        private readonly IOperateLogService _service;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service"></param>
        public OperateLogController(IOperateLogService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取操作日志列表数据
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<OperateLogQueryModel>> GetPageAsync(OperateLogQueryParam param)
        {
            return await _service.QueryPageAsync(param);
        }
    }
}
