using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.DependencyInjection;
using Hx.Sdk.Entity.Page;
using Hx.Sdk.Extensions;
using HxCore.Entity.Entities;
using HxCore.Model.Admin.OperateLog;
using HxCore.Model.NotificationHandlers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HxCore.Entity;
using HxCore.IServices;

namespace HxCore.Services
{
    /// <summary>
    /// 操作日志记录Service
    /// </summary>
    public class OperateLogService : Internal.PrivateService<T_OperateLog, MasterDbContextLocator>, IOperateLogService, IScopedDependency
    {
        public OperateLogService(IRepository<T_OperateLog, MasterDbContextLocator> userDal) : base(userDal)
        {
        }

        public async Task<bool> AddOperateLog(AddOperateLogModel model)
        {
            var optLog = this.Mapper.Map<T_OperateLog>(model);
            optLog.OperaterId = UserId;
            optLog.Operater = UserName;
            return await this.InsertAsync(optLog);
        }
       
    }
}
