using HxCore.IServices;
using HxCore.Model.Admin.Banner;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers.Admin
{
    /// <summary>
    /// 首页横幅控制器
    /// </summary>
    public class BannerController : BaseAdminController
    {
        private readonly IBannerService _service;
        private readonly IBannerQuery _query;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service">服务类接口</param>
        /// <param name="query">查询类接口</param>
        public BannerController(IBannerService service, IBannerQuery query)
        {
            _service = service;
            _query = query;
        }

        #region 查询
        /// <summary>
        /// 获取首页横幅分页列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<SqlSugarPageModel<BannerQueryModel>> GetPageAsync(BannerQueryParam param)
        {
            var result = await _query.QueryNoticePageAsync(param);
            return result;
        }

        /// <summary>
        /// 获取首页横幅详情数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<BannerDetailModel> GetAsync(string id)
        {
            var result = await _query.GetDetailAsync(id);
            return result;
        }
        #endregion

        #region 操作
        /// <summary>
        /// 添加首页横幅
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddAsync(BannerCreateModel createModel)
        {
            return await _service.InsertAsync(createModel);
        }

        /// <summary>
        /// 编辑首页横幅
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAsync(BannerCreateModel updateModel)
        {
            return await _service.UpdateAsync(updateModel);
        }

        /// <summary>
        /// 删除首页横幅
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
