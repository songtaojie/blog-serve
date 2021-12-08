using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.Banner;
using HxCore.Model.Client;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class BannerQuery : BaseQuery<T_BannerInfo>, IBannerQuery
    {
        public BannerQuery(ISqlSugarRepository<T_BannerInfo> repository) : base(repository)
        {
        }
        #region 后台管理
        public async Task<SqlSugarPageModel<BannerManageQueryModel>> QueryNoticePageAsync(BannerManageQueryParam param)
        {
            var query = this.Db.Queryable<T_BannerInfo>().Where(r => r.Deleted == ConstKey.No)
                   .OrderBy(r => r.OrderSort, OrderByType.Desc)
                   .OrderBy(r => r.CreateTime, OrderByType.Desc)
                   .Select(r => new BannerManageQueryModel
                   {
                       Id = r.Id,
                       Title = r.Title,
                       Link = r.Link,
                       ImgUrl = r.ImgUrl,
                       OrderSort = r.OrderSort,
                       Target = r.Target,
                       IsEnabled = r.Disabled == ConstKey.No
                   });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }
        public async Task<BannerManageDetailModel> GetDetailAsync(string id)
        {
            var detailModel = await this.Repository.Entities.Where(r=>r.Id == id)
                .Select(r => new BannerManageDetailModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Link = r.Link,
                    ImgUrl = r.ImgUrl,
                    OrderSort = r.OrderSort,
                    Target = r.Target,
                    IsEnabled = r.Disabled == ConstKey.No
                })
                .FirstAsync(r => r.Id == id);
            if (detailModel == null) throw new UserFriendlyException("该条数据不存在", ErrorCodeEnum.DataNull);
            return detailModel;
        }
        #endregion

        #region 客户端
        public async Task<List<BannerModel>> GetListAsync(int count)
        {
            return await this.Db.Queryable<T_BannerInfo>()
                    .Where(r => r.Deleted == ConstKey.No && r.Disabled == ConstKey.No)
                    .OrderBy(r => r.OrderSort, OrderByType.Desc)
                    .OrderBy(r => r.CreateTime, OrderByType.Desc)
                    .Take(count)
                    .Select(r => new BannerModel
                    {
                        Title = r.Title,
                        Link = r.Link,
                        ImgUrl = r.ImgUrl,
                        Target = r.Target
                    }).ToListAsync();
        }
        #endregion
    }
}
