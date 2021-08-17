using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.DependencyInjection;
using Hx.Sdk.Entity.Page;
using Hx.Sdk.Extensions;
using HxCore.Entity.Entities;
using HxCore.IServices.Admin;
using HxCore.Model.Admin.OperateLog;
using HxCore.Model.NotificationHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services.Admin
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
            optLog.OperaterId = this.UserContext.UserId;
            optLog.Operater = this.UserContext.UserName;
            return await this.InsertAsync(optLog);
        }

        /// <inheritdoc cref="IOperateLogService.QueryPageAsync"/>
        public async Task<PageModel<OperateLogQueryModel>> QueryPageAsync(OperateLogQueryParam param)
        {
            return await this.Repository.DetachedEntities
                .OrderByDescending(o=>o.OperateTime)
                .Select(o => this.Mapper.Map<OperateLogQueryModel>(o))
                .ToOrderAndPageListAsync(param);
        }
    }
}
