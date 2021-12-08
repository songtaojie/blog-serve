using HxCore.Model.Admin.Banner;
using HxCore.Model.Client;
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
        #region 后台管理
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
        #endregion

        #region 管理端
        /// <summary>
        /// 获取首页横幅列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        Task<List<BannerModel>> GetListAsync(int count);
        #endregion
    }
}
