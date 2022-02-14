using Hx.Sdk.Entity.Page;
using HxCore.Entity.Entities;
using HxCore.Model.Admin.OperateLog;
using HxCore.Model.NotificationHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 操作日志接口
    /// </summary>
    public interface IOperateLogService : IBaseService<T_OperateLog>
    {
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddOperateLog(AddOperateLogModel model);
    }
}
