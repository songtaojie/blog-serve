using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model.Admin.Menu;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services.Admin.Query
{
    public class MenuQuery : BaseQuery<T_Menu>, IMenuQuery
    {
        public MenuQuery(ISqlSugarRepository<T_Menu> repository) : base(repository)
        {
        }

        public async Task<SqlSugarPageModel<MenuQueryModel>> QueryNoticePageAsync(MenuQueryParam param)
        {
            var query = this.Db.Queryable<T_BannerInfo>().Where(r => r.Deleted == ConstKey.No)
                   .OrderBy(r => r.OrderIndex, OrderByType.Desc)
                   .OrderBy(r => r.CreateTime, OrderByType.Desc)
                   .Select(r => new MenuQueryModel
                   {
                       Id = r.Id,
                       IsEnabled = r.Disabled == ConstKey.No
                   });
            return await query.ToPagedListAsync(param.PageIndex, param.PageSize);
        }
        public async Task<MenuDetailModel> GetDetailAsync(string id)
        {
            var test = await this.Db.Queryable<T_MenuModule>().FirstAsync();


            var detailModel = await this.Repository.Entities.Where(r => r.Id == id)
                .Select(r => new MenuDetailModel
                {
                    Id = r.Id,
                    IsEnabled = r.Disabled == ConstKey.No
                })
                .FirstAsync(r => r.Id == id);
            if (detailModel == null) throw new UserFriendlyException("该条数据不存在", ErrorCodeEnum.DataNull);
            return detailModel;
        }

    }
}
