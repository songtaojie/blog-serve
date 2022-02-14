using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.OperateLog;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class OperateLogQuery : BaseQuery<T_OperateLog>, IOperateLogQuery
    {
        private readonly IUserService _userService;
        public OperateLogQuery(ISqlSugarRepository<T_OperateLog> repository,IUserService userService) : base(repository)
        {
            _userService = userService; 
        }

        public async Task<OperateLogChars> GetLineDataAsync()
        {
            var beginDate = DateTime.Now.AddDays(-30);
            var endDate = DateTime.Now;
            var userId = UserContext.UserId;
            var isSuperAdmin = await _userService.CheckIsSuperAdminAsync(userId);

            var rows = await this.Db.Queryable<T_OperateLog, T_Module>((op, m) => new JoinQueryInfos(JoinType.Left, op.ControllerName == m.Controller && op.ActionName == m.Action))
                    .WhereIF(isSuperAdmin ,(op, m) => m.Deleted == ConstKey.No)
                    .WhereIF(!isSuperAdmin, (op, m) => m.Deleted == ConstKey.No && op.OperaterId == userId)
                    .GroupBy((op, m) => new { op.ControllerName, op.ActionName, m.Description })
                    .Select((op, m) => new
                    {
                        name = SqlFunc.IsNullOrEmpty(m.Description) 
                            ? SqlFunc.MergeString(op.ControllerName, op.ActionName)
                            : SqlFunc.MergeString(m.Description,"[", op.ControllerName, ".", op.ActionName, "]"),
                        count= SqlFunc.AggregateCount(op.Id)
                    })
                    .ToListAsync();
            return new OperateLogChars
            {
                columns = new string[] { "name", "count" },
                rows = rows
            };
        }

        public async Task<SqlSugarPageModel<OperateLogQueryModel>> QueryPageAsync(OperateLogQueryParam param)
        {
            var userId = UserContext.UserId;
            var isSuperAdmin = await _userService.CheckIsSuperAdminAsync(userId);
            var result = await this.Repository.Entities
                   .WhereIF(!isSuperAdmin, op => op.OperaterId == userId)
                   .OrderBy(op => op.OperateTime, OrderByType.Desc)
                   .Select(op => new OperateLogQueryModel
                   {
                       Id = op.Id,
                       Agent = op.Agent,
                       IPAddress = op.IPAddress,
                       OperateTime = op.OperateTime,
                       ElapsedTime = op.ElapsedTime,
                       HttpMethod = op.HttpMethod,
                       Location = op.Location,
                       Operater = op.Operater,
                       Success = op.Success,
                       Url = op.Url,
                       Param = param.IsWelcome ? null:op.Param,
                       Result = param.IsWelcome ? null : op.Result,
                   })
                   .ToPagedListAsync(param.PageIndex, param.PageSize);
            return result;
        }
    }
}
