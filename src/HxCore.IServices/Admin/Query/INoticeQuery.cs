using HxCore.Model.Admin.Notice;
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
    /// 公告通知查询类
    /// </summary>
    public interface INoticeQuery
    {
        #region 后台管理
        /// <summary>
        /// 获取公告通知分页列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<NoticeManageQueryModel>> QueryNoticePageAsync(NoticeManageQueryParam param);

        /// <summary>
        /// 获取公告通知详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<NoticeManageDetailModel> GetDetailAsync(string id);
        #endregion

        #region 客户端
        /// <summary>
        /// 获取公告通知列表数据
        /// </summary>
        /// <param name="count">要获取的条数</param>
        /// <returns></returns>
        Task<List<NoticeModel>> GetListAsync(int count);
        #endregion
    }
}
