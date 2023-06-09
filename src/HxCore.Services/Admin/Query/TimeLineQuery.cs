﻿using Hx.Sdk.Entity.Page;
using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.TimeLine;
using HxCore.Model.Client;
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
        #region 客户端
        public async Task<SqlSugarPageModel<TimeLineModel>> GetPageAsync(int pageIndex,int pageSize)
        {
            var query = this.Repository.Entities
                   .Where(r => r.Deleted == ConstKey.No)
                   .OrderBy(r => r.CreateTime, OrderByType.Desc)
                   .Select(r => new TimeLineModel
                   {
                       Content = r.Content,
                       Link = r.Link,
                       Target = r.Target,
                       Timestamp = r.LastModifyTime
                   });
            return await query.ToPagedListAsync(pageIndex, pageSize);
        }
        #endregion
        #region 管理后台
        public async Task<SqlSugarPageModel<TimeLineManageQueryModel>> QueryTimeLinePageAsync(TimeLineManageQueryParam param)
        {
            var query = this.Repository.Entities
                   .Where(r => r.Deleted == ConstKey.No)
                   .OrderBy(r => r.CreateTime, OrderByType.Desc)
                   .Select(r => new TimeLineManageQueryModel
                   {
                       Id = r.Id,
                       Content = r.Content,
                       Link = r.Link,
                       Target = r.Target,
                       IsEnabled = r.Disabled == ConstKey.No
                   });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }
        public async Task<TimeLineManageDetailModel> GetDetailAsync(string id)
        {
            var detailModel = await this.Repository.Entities.Where(r=>r.Id == id)
                .Select(r => new TimeLineManageDetailModel
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
        #endregion

    }
}
