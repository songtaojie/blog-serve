using HxCore.Model.Admin.Menu;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 菜单查询类
    /// </summary>
    public interface IMenuQuery
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<MenuQueryModel>> QueryNoticePageAsync(MenuQueryParam param);

        /// <summary>
        /// 获取菜单详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MenuDetailModel> GetDetailAsync(string id);
    }
}
