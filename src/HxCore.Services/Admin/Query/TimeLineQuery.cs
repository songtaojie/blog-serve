using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.TimeLine;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services.Admin.Query
{
    public class TimeLineQuery : BaseQuery<T_TimeLine>, ITimeLineQuery
    {
        public TimeLineQuery(ISqlSugarRepository<T_TimeLine> repository) : base(repository)
        {
        }

        public async Task<SqlSugarPageModel<TimeLineQueryModel>> QueryTimeLinePageAsync(TimeLineQueryParam param)
        {
            var query = this.Repository.Entities
                   .Where(r => r.Deleted == ConstKey.No)
                   .OrderBy(r => r.CreateTime, OrderByType.Desc)
                   .Select(r => new TimeLineQueryModel
                   {
                       Id = r.Id,
                       Content = r.Content,
                       Link = r.Link,
                       Target = r.Target,
                       IsEnabled = r.Disabled == ConstKey.No
                   });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }
        public async Task<TimeLineDetailModel> GetDetailAsync(string id)
        {
            var detailModel = await this.Repository.Entities.Where(r=>r.Id == id)
                .Select(r => new TimeLineDetailModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    Link = r.Link,
                    Target = r.Target,
                    IsEnabled = r.Disabled == ConstKey.No
                })
                .FirstAsync(r => r.Id == id);
            if (detailModel == null) throw new UserFriendlyException("该条数据不存在", ErrorCodeEnum.DataNull);
            return detailModel;
        }

    }
}
