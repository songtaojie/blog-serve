using HxCore.Model.Admin.FriendLink;
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
    /// 友情链接查询接口
    /// </summary>
    public interface IFriendLinkQuery
    {
        #region 管理后台
        /// <summary>
        /// 获取友情链接列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<FriendLinkManageQueryModel>> QueryFriendLinkPageAsync(FriendLinkManageQueryParam param);

        /// <summary>
        /// 获取友情链接详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FriendLinkManageDetailModel> GetDetailAsync(string id);
        #endregion

        #region 客户端
        /// <summary>
        /// 获取公告通知列表数据
        /// </summary>
        /// <param name="count">要获取的条数</param>
        /// <returns></returns>
        Task<List<FriendLinkModel>> GetListAsync();
        #endregion
    }
}
