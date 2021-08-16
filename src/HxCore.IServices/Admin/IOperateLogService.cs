using HxCore.Entity.Entities;
using HxCore.Model.NotificationHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices.Admin
{
    /// <summary>
    /// 操作日志接口
    /// </summary>
    public interface IOperateLogService : IBaseService<T_OperateLog>
    {
        Task<bool> AddOperateLog(AddOperateLogModel model);
    }
}
