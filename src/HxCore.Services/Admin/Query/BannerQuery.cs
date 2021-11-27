using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.Banner;
using SqlSugar;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class BannerQuery : BaseQuery<T_BannerInfo>, IBannerQuery
    {
        public BannerQuery(ISqlSugarRepository<T_BannerInfo> repository) : base(repository)
        {
        }

        public async Task<SqlSugarPageModel<BannerQueryModel>> QueryNoticePageAsync(BannerQueryParam param)
        {
            var query = this.Repository.Entities.Where(r => r.Deleted == ConstKey.No)
                   .OrderBy(r => r.OrderIndex, OrderByType.Desc)
                   .OrderBy(r => r.CreateTime, OrderByType.Desc)
                   .Select(r => new BannerQueryModel
                   {
                       Id = r.Id,
                       Title = r.Title,
                       Link = r.Link,
                       ImgUrl = r.ImgUrl,
                       OrderIndex = r.OrderIndex,
                       Target = r.Target,
                       IsEnabled = r.Disabled == ConstKey.No
                   });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }
        public async Task<BannerDetailModel> GetDetailAsync(string id)
        {
            var entity = await this.Repository.FirstOrDefaultAsync(r => r.Id == id);
            if (entity == null) throw new UserFriendlyException("该条数据不存在", ErrorCodeEnum.DataNull);
            var detailModel = this.Mapper.Map(entity, new BannerDetailModel
            {
                IsEnabled = entity.Disabled == ConstKey.No
            });
            return detailModel;
        }

    }
}
