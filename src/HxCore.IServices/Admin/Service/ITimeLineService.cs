using HxCore.Entity.Entities;
using HxCore.Model.Admin.TimeLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 时间轴服务接口
    /// </summary>
    public interface ITimeLineService : IBaseStatusService<T_TimeLine>
    {
        /// <summary>
        /// 插入时间轴
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(TimeLineCreateModel createModel);

        /// <summary>
        /// 更新时间轴
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TimeLineCreateModel updateModel);
    }
}
