using HxCore.IServices;
using HxCore.Model.Admin.TimeLine;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers.Admin
{

    /// <summary>
    /// 时间轴控制器
    /// </summary>
    public class TimeLineManageController : BaseAdminController
    {
        private readonly ITimeLineService _service;
        private readonly ITimeLineQuery _query;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service">服务类接口</param>
        /// <param name="query">查询类接口</param>
        public TimeLineManageController(ITimeLineService service, ITimeLineQuery query)
        {
            _service = service;
            _query = query;
        }

        #region 查询
        /// <summary>
        /// 获取时间轴分页列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<SqlSugarPageModel<TimeLineManageQueryModel>> GetPageAsync(TimeLineManageQueryParam param)
        {
            var result = await _query.QueryTimeLinePageAsync(param);
            return result;
        }

        /// <summary>
        /// 获取时间轴详情数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<TimeLineManageDetailModel> GetAsync(string id)
        {
            var result = await _query.GetDetailAsync(id);
            return result;
        }
        #endregion

        #region 操作
        /// <summary>
        /// 添加时间轴
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddAsync(TimeLineManageCreateModel createModel)
        {
            return await _service.InsertAsync(createModel);
        }

        /// <summary>
        /// 编辑时间轴
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAsync(TimeLineManageCreateModel updateModel)
        {
            return await _service.UpdateAsync(updateModel);
        }

        /// <summary>
        /// 删除时间轴
        /// </summary>
        /// <param name="id">要删除的时间轴的id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(string id)
        {
            return await _service.DeleteAsync(id);
        }

        #endregion
    }
}
