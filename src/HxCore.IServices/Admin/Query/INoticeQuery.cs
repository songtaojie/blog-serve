using HxCore.Model.Admin.Notice;
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
        /// <summary>
        /// 获取友情链接列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<NoticeManageQueryModel>> QueryNoticePageAsync(NoticeManageQueryParam param);

        /// <summary>
        /// 获取友情链接详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<NoticeManageDetailModel> GetDetailAsync(string id);
    }
}
