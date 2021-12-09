using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.Notice;
using HxCore.Model.Client;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class NoticeQuery : BaseQuery<T_NoticeInfo>, INoticeQuery
    {
        public NoticeQuery(ISqlSugarRepository<T_NoticeInfo> repository) : base(repository)
        {
        }

        #region 管理后台
        public async Task<SqlSugarPageModel<NoticeManageQueryModel>> QueryNoticePageAsync(NoticeManageQueryParam param)
        {
            var query = this.Db.Queryable<T_NoticeInfo>()
                   .Where(r => r.Deleted == ConstKey.No)
                   .OrderBy(r => r.OrderSort, OrderByType.Desc)
                   .OrderBy(r => r.CreateTime, OrderByType.Desc)
                   .Select(r => new NoticeManageQueryModel
                   {
                       Id = r.Id,
                       Link = r.Link,
                       Content = r.Content,
                       OrderSort = r.OrderSort,
                       Target = r.Target,
                       IsEnabled = r.Disabled == ConstKey.No
                   });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }
        public async Task<NoticeManageDetailModel> GetDetailAsync(string id)
        {
            var detailModel = await this.Repository.Entities.Where(r => r.Id == id)
                .Select(r => new NoticeManageDetailModel
                {
                    Id = r.Id,
                    Link = r.Link,
                    Content = r.Content,
                    OrderSort = r.OrderSort,
                    Target = r.Target,
                    IsEnabled = r.Disabled == ConstKey.No
                })
                .FirstAsync(f => f.Id == id);
            if (detailModel == null) throw new UserFriendlyException("该公告通知不存在", ErrorCodeEnum.DataNull);
            return detailModel;
        }
        #endregion

        public async Task<List<NoticeModel>> GetListAsync(int count)
        {
            return await this.Db.Queryable<T_NoticeInfo>()
                  .Where(r => r.Deleted == ConstKey.No && r.Disabled == ConstKey.No)
                  .OrderBy(r => r.OrderSort, OrderByType.Desc)
                  .OrderBy(r => r.CreateTime, OrderByType.Desc)
                  .Select(r => new NoticeModel
                  {
                      Id = r.Id,
                      Link = r.Link,
                      Content = r.Content,
                      Target = r.Target
                  })
                  .ToListAsync();
        }
    }
}
