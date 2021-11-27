using HxCore.Model.Admin.FriendLink;
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
        /// <summary>
        /// 获取友情链接列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<FriendLinkQueryModel>> QueryFriendLinkPageAsync(FriendLinkQueryParam param);

        /// <summary>
        /// 获取友情链接详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FriendLinkDetailModel> GetDetailAsync(string id);
    }
}
