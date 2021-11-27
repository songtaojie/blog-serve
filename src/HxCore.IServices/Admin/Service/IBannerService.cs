using HxCore.Entity.Entities;
using HxCore.Model.Admin.Banner;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 首页横幅服务接口
    /// </summary>
    public interface IBannerService : IBaseStatusService<T_BannerInfo>
    {
        /// <summary>
        /// 插入首页横幅
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(BannerCreateModel createModel);

        /// <summary>
        /// 更新首页横幅
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BannerCreateModel updateModel);
    }
}
