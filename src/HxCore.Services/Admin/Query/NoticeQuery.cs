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
            var query = this.Repository.Entities.Where(n => n.Deleted == ConstKey.No)
                   .OrderBy(n => n.OrderIndex, OrderByType.Desc)
                   .OrderBy(n => n.CreateTime, OrderByType.Desc)
                   .Select(n => new NoticeQueryModel
                   {
                       Id = n.Id,
                       Link = n.Link,
                       Content = n.Content,
                       OrderIndex = n.OrderIndex,
                       Target = n.Target,
                       IsEnabled = n.Disabled == ConstKey.No
                   });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }
        public async Task<NoticeDetailModel> GetDetailAsync(string id)
        {
            var entity = await this.Repository.FirstOrDefaultAsync(f => f.Id == id);
            if (entity == null) throw new UserFriendlyException("该公告通知不存在", ErrorCodeEnum.DataNull);
            var detailModel = this.Mapper.Map(entity, new NoticeDetailModel
            {
                IsEnabled = entity.Disabled == ConstKey.No
            });
            return detailModel;
        }

    }
}
