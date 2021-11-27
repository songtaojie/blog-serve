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
            var query = this.Repository.Entities.Where(f => f.Deleted == ConstKey.No)
                    .OrderBy(f => f.OrderIndex, OrderByType.Desc)
                    .OrderBy(f => f.CreateTime, OrderByType.Desc)
                    .Select(f => new FriendLinkQueryModel
                    {
                        Id = f.Id,
                        Link = f.Link,
                        SiteName = f.SiteName,
                        OrderIndex = f.OrderIndex,
                        Logo = f.Logo,
                        IsEnabled = f.Disabled ==  ConstKey.No
                    });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }

        public async Task<FriendLinkDetailModel> GetDetailAsync(string id)
        {
            var entity = await this.Repository.FirstOrDefaultAsync(f => f.Id == id);
            if (entity == null) throw new UserFriendlyException("该友情链接不存在", ErrorCodeEnum.DataNull);
            var detailModel = this.Mapper.Map(entity,new FriendLinkDetailModel
            {
                IsEnabled = entity.Disabled ==  ConstKey.No
            });
            return detailModel;
        }
        #endregion
    }
}
