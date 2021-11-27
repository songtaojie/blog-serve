using HxCore.Entity.Entities;
using HxCore.Model.Admin.FriendLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 友情链接服务类接口
    /// </summary>
    public interface IFriendLinkService : IBaseStatusService<T_FriendLink>
    {
        /// <summary>
        /// 插入友情链接
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(FriendLinkCreateModel createModel);

        /// <summary>
        /// 更新友情链接
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(FriendLinkCreateModel updateModel);
    }
}
