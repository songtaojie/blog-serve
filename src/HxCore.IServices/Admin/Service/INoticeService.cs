using HxCore.Entity.Entities;
using HxCore.Model.Admin.Notice;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 公告通知服务接口
    /// </summary>
    public interface  INoticeService : IBaseStatusService<T_NoticeInfo>
    {
        /// <summary>
        /// 插入公告通知
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(NoticeManageCreateModel createModel);

        /// <summary>
        /// 更新公告通知
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(NoticeManageCreateModel updateModel);
    }
}
