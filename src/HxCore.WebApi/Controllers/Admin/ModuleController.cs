using Hx.Sdk.Entity.Page;
using HxCore.Extensions.Common;
using HxCore.Extensions.Document;
using HxCore.IServices.Admin;
using HxCore.Model.Admin.Module;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using System.Reflection;

namespace HxCore.WebApi.Controllers.Admin
{
    /// <summary>
    /// 接口api控制器
    /// </summary>
    public class ModuleController : BaseAdminController
    {
        private readonly IModuleService _service;
        private IActionDescriptorCollectionProvider _actionProvider;
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="actionProvider"></param>
        public ModuleController(IModuleService service, IActionDescriptorCollectionProvider actionProvider)
        {
            _service = service;
            _actionProvider = actionProvider;
        }

        #region 查询
        /// <summary>
        /// 获取接口分页列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageModel<ModuleQueryModel>> GetPageAsync(ModuleQueryParam param)
        {
            var result = await _service.QueryModulePageAsync(param);
            return result;
        }

        /// <summary>
        /// 获取接口列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ModuleQueryModel>> GetListAsync(ModuleQueryParam param)
        {
            var result = await _service.GetListAsync(param);
            return result;
        }

        /// <summary>
        /// 获取接口详情数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ModuleDetailModel> GetAsync(string id)
        {
            var result = await _service.GetAsync(id);
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
        public async Task<bool> AddAsync(ModuleCreateModel createModel)
        {
            return await _service.AddAsync(createModel);
        }

        /// <summary>
        /// 编辑接口
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAsync(ModuleUpdateModel createModel)
        {
            return await _service.UpdateAsync(createModel);
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

        /// <summary>
        /// 同步接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<bool> SyncInterface()
        {
            List<ModuleCreateModel> moduleCreates = GetActionDescription();
            return await this._service.BatchAddOrUpdateAsync(moduleCreates);
        }


        /// <summary>
        /// 获取action的信息
        /// </summary>
        /// <returns></returns>
        private  List<ModuleCreateModel> GetActionDescription()
        {
            var type = this.GetType();
            var xmlName = string.Format("{0}.xml", type.Assembly.GetName().Name);
            string MemberXPath = "/doc/members/member[@name='{0}']";
            string SummaryTag = "summary";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlName);
            if (!System.IO.File.Exists(path)) return new List<ModuleCreateModel>();
            XPathDocument xmlDoc = new XPathDocument(path);
            XPathNavigator _xmlNavigator = xmlDoc.CreateNavigator();
            var actionNamesAndTypes = _actionProvider.ActionDescriptors.Items
                .Cast<ControllerActionDescriptor>()
                .SkipWhile(actionDesc => actionDesc == null)
                .Select(actionDesc => new ModuleCreateModel
                {
                    RouteUrl = actionDesc.AttributeRouteInfo.Template,
                    Controller = actionDesc.ControllerName,
                    Action = actionDesc.ActionName,
                    MethodInfo = actionDesc.MethodInfo
                }).ToList();
            var httpMethodType = typeof(HttpMethodAttribute);
            foreach (var action in actionNamesAndTypes)
            {
                var httpMethodAttributes = action.MethodInfo.GetCustomAttributes<HttpMethodAttribute>();
                if (httpMethodAttributes.Any())
                {
                    var httpMethods = httpMethodAttributes.SelectMany(r => r.HttpMethods);
                    action.HttpMethod = string.Join(",", string.Join(",", httpMethods));
                }
                action.Name = string.Format("{0}.{1}", action.MethodInfo.DeclaringType.FullName, action.MethodInfo.Name);
                var methodMemberName = XmlCommentsNodeNameHelper.GetMemberNameForMethod(action.MethodInfo);
                var methodNode = _xmlNavigator.SelectSingleNode(string.Format(MemberXPath, methodMemberName));

                if (methodNode != null)
                {
                    var summaryNode = methodNode.SelectSingleNode(SummaryTag);
                    if (summaryNode != null)
                    {
                        action.Description = XmlCommentsTextHelper.Humanize(summaryNode.InnerXml);
                    }
                }
            }
            return actionNamesAndTypes;
        }
        #endregion
    }
}
