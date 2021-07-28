using HxCore.IServices.Admin;
using HxCore.Model.Admin;
using HxCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.Web.Controllers.Admin
{
    /// <summary>
    /// 菜单按钮权限控制器
    /// </summary>
    public class PermissionController: BaseAdminController
    {
        private readonly IPermissionService _service;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service"></param>
        public PermissionController(IPermissionService service)
        {
            _service = service;
        }
        #region 查询

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MenuQueryModel>> GetList()
        {
            return await _service.GetListAsync();
        }


        /// <summary>
        /// 获取路由树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Entity.Permission.Permission("Permission.Router")]
        [Authorize()]
        public async Task<List<RouterQueryModel>> GetRouters()
        {
            return  await _service.GetRoutersAsync();
        }

        /// <summary>
        /// 获取菜单下拉树列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>添加菜单时候的上级菜单下拉列表</remarks>
        [HttpGet]
        public async Task<List<MenuPullDownModel>> GetUserMenuTree()
        {
            return await _service.GetUserMenuTreeAsync();
        }

        /// <summary>
        /// 获取接口列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<MenuDetailModel> Get(string id)
        {
            var result = await _service.GetAsync(id);
            return result;
        }
        
        #endregion

        #region 操作
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(MenuCreateModel createModel)
        {
            return await _service.AddAsync(createModel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Update(MenuCreateModel createModel)
        {
            return await _service.UpdateAsync(createModel);
        }

        /// <summary>
        /// 接口删除
        /// </summary>
        /// <param name="id">要删除的接口的id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _service.DeleteAsync(id);
        }
        #endregion
    }
}
