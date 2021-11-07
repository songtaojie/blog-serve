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
using Microsoft.EntityFrameworkCore;
using HxCore.Entity;

namespace HxCore.Services.Admin
{
    /// <summary>
    /// 操作日志记录Service
    /// </summary>
    public class OperateLogService : Internal.PrivateService<T_OperateLog, MasterDbContextLocator>, IOperateLogService, IScopedDependency
    {
        private IUserService _userService;
        public OperateLogService(IRepository<T_OperateLog, MasterDbContextLocator> userDal, IUserService userService) : base(userDal)
        {
            _userService = userService;
        }

        public async Task<bool> AddOperateLog(AddOperateLogModel model)
        {
            var optLog = this.Mapper.Map<T_OperateLog>(model);
            optLog.OperaterId = this.UserContext.UserId;
            optLog.Operater = this.UserContext.UserName;
            return await this.InsertAsync(optLog);
        }

        /// <inheritdoc cref="IOperateLogService.GetLineDataAsync"/>
        public async Task<OperateLogChars> GetLineDataAsync()
        {
            var beginDate = DateTime.Now.AddDays(-30);
            var endDate = DateTime.Now;
            var isSuperAdmin = await _userService.CheckIsSuperAdminAsync(UserContext.UserId);
            var rows = await (from op in this.Repository.DetachedEntities
                        join m in this.Db.Set<T_Module>() on new {op.ControllerName,op.ActionName } equals new { ControllerName = m.Controller, ActionName = m.Action } into m_temp
                        from m in m_temp.DefaultIfEmpty()
                        where isSuperAdmin?m.Deleted == ConstKey.No: m.Deleted == ConstKey.No && op.OperaterId == UserContext.UserId
                        group op by new { op.ControllerName, op.ActionName, m.Description } into opgp
                        select new
                        { 
                            name = string.IsNullOrEmpty(opgp.Key.Description)
                            ? string.Format("{0}.{1}", opgp.Key.ControllerName, opgp.Key.ActionName)
                            : string.Format("{0}[{1}.{2}]", opgp.Key.Description, opgp.Key.ControllerName, opgp.Key.ActionName),
                            count = opgp.Count()
                        }).ToListAsync();
            return new OperateLogChars
            {
                columns = new string[] { "name", "count" },
                rows = rows
            };
        }

        /// <inheritdoc cref="IOperateLogService.QueryPageAsync"/>
        public async Task<PageModel<OperateLogQueryModel>> QueryPageAsync(OperateLogQueryParam param)
        {
            if (param.IsWelcome)
            {
                var isSuperAdmin = await _userService.CheckIsSuperAdminAsync(UserContext.UserId);
                if (!isSuperAdmin)
                {
                    return await this.Repository.DetachedEntities
                        .Where(o=>o.OperaterId == UserContext.UserId)
                       .OrderByDescending(o => o.OperateTime)
                       .Select(o => this.Mapper.Map<OperateLogQueryModel>(o))
                       .ToOrderAndPageListAsync(param);
                }
            }
            
            return await this.Repository.DetachedEntities
                .OrderByDescending(o=>o.OperateTime)
                .Select(o => this.Mapper.Map<OperateLogQueryModel>(o))
                .ToOrderAndPageListAsync(param);
        }
    }
}
