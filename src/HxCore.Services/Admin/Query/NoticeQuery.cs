using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.Notice;
using SqlSugar;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class NoticeQuery : BaseQuery<T_NoticeInfo>, INoticeQuery
    {
        public NoticeQuery(ISqlSugarRepository<T_NoticeInfo> repository) : base(repository)
        {
        }
       
        public async Task<SqlSugarPageModel<NoticeQueryModel>> QueryNoticePageAsync(NoticeQueryParam param)
        {
            var query = this.Db.Queryable<T_NoticeInfo>()
                   .Where(r => r.Deleted == ConstKey.No)
                   .OrderBy(r => r.OrderSort, OrderByType.Desc)
                   .OrderBy(r => r.CreateTime, OrderByType.Desc)
                   .Select(r => new NoticeQueryModel
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
        public async Task<NoticeDetailModel> GetDetailAsync(string id)
        {
            var detailModel = await this.Repository.Entities.Where(r=>r.Id == id)
                .Select(r => new NoticeDetailModel
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

    }
}
