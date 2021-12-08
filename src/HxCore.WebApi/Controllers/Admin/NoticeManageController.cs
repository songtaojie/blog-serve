using HxCore.IServices;
using HxCore.Model.Admin.Notice;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers.Admin
{
    /// <summary>
    /// 公告通知控制器
    /// </summary>
    public class NoticeManageController : BaseAdminController
    {
        private readonly INoticeService _service;
        private readonly INoticeQuery _query;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service">服务类接口</param>
        /// <param name="query">查询类接口</param>
        public NoticeManageController(INoticeService service, INoticeQuery query)
        {
            _service = service;
            _query = query;
        }

        #region 查询
        /// <summary>
        /// 获取友情链接分页列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<SqlSugarPageModel<NoticeManageQueryModel>> GetPageAsync(NoticeManageQueryParam param)
        {
            var result = await _query.QueryNoticePageAsync(param);
            return result;
        }

        /// <summary>
        /// 获取接口详情数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<NoticeManageDetailModel> GetAsync(string id)
        {
            var result = await _query.GetDetailAsync(id);
            return result;
        }
        #endregion

        #region 操作
        /// <summary>
        /// 添加接口
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddAsync(NoticeManageCreateModel createModel)
        {
            return await _service.InsertAsync(createModel);
        }

        /// <summary>
        /// 编辑接口
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAsync(NoticeManageCreateModel updateModel)
        {
            return await _service.UpdateAsync(updateModel);
        }

        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="id">要删除的接口的id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(string id)
        {
            return await _service.DeleteAsync(id);
        }

        #endregion
    }
}
