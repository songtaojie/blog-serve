using HxCore.IServices;
using HxCore.Model.Admin.FriendLink;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;

namespace HxCore.WebApi.Controllers.Admin
{
    /// <summary>
    /// 友情链接控制器
    /// </summary>
    public class FriendLinkController : BaseAdminController
    {
        private readonly IFriendLinkService _service;
        private readonly IFriendLinkQuery _query;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service">服务类接口</param>
        /// <param name="query">查询类接口</param>
        public FriendLinkController(IFriendLinkService service, IFriendLinkQuery query)
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
        public async Task<SqlSugarPageModel<FriendLinkQueryModel>> GetPageAsync(FriendLinkQueryParam param)
        {
            var result = await _query.QueryFriendLinkPageAsync(param);
            return result;
        }

        /// <summary>
        /// 获取接口详情数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<FriendLinkDetailModel> GetAsync(string id)
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
        public async Task<bool> AddAsync(FriendLinkCreateModel createModel)
        {
            return await _service.InsertAsync(createModel);
        }

        /// <summary>
        /// 编辑接口
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAsync(FriendLinkCreateModel updateModel)
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
