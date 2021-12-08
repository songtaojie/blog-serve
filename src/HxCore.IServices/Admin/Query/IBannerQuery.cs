using HxCore.Model.Admin.Banner;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 首页横幅查询类
    /// </summary>
    public interface IBannerQuery
    {
        /// <summary>
        /// 获取首页横幅列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<BannerManageQueryModel>> QueryNoticePageAsync(BannerManageQueryParam param);

        /// <summary>
        /// 获取首页横幅详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BannerManageDetailModel> GetDetailAsync(string id);
    }
}
