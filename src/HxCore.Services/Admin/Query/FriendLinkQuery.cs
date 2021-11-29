using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.FriendLink;
using SqlSugar;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class FriendLinkQuery : BaseQuery<T_FriendLink>, IFriendLinkQuery
    {
        public FriendLinkQuery(ISqlSugarRepository<T_FriendLink> repository) : base(repository)
        {
        }

        #region 后台管理-查询
        /// <summary>
        /// 获取友情链接列表
        /// </summary>
        /// <returns></returns>
        public async Task<SqlSugarPageModel<FriendLinkQueryModel>> QueryFriendLinkPageAsync(FriendLinkQueryParam param)
        {
            var query = this.Repository.Entities.Where(r => r.Deleted == ConstKey.No)
                    .OrderBy(r => r.OrderIndex, OrderByType.Desc)
                    .OrderBy(r => r.CreateTime, OrderByType.Desc)
                    .Select(r => new FriendLinkQueryModel
                    {
                        Id = r.Id,
                        Link = r.Link,
                        SiteCode = r.SiteCode,
                        SiteName = r.SiteName,
                        OrderIndex = r.OrderIndex,
                        Logo = r.Logo,
                        IsEnabled = r.Disabled ==  ConstKey.No
                    });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }

        public async Task<FriendLinkDetailModel> GetDetailAsync(string id)
        {
            var detailModel = await this.Repository.Entities
                .Where(r => r.Id == id)
                .Select(r => new FriendLinkDetailModel
                {
                    Id = r.Id,
                    Link = r.Link,
                    SiteCode = r.SiteCode,
                    SiteName = r.SiteName,
                    OrderIndex = r.OrderIndex,
                    Logo = r.Logo,
                    IsEnabled = r.Disabled == ConstKey.No
                }).FirstAsync();
            if (detailModel == null) throw new UserFriendlyException("该友情链接不存在", ErrorCodeEnum.DataNull);
            return detailModel;
        }
        #endregion
    }
}
