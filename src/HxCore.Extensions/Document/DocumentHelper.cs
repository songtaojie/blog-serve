using HxCore.Model.Admin.Module;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Xml.XPath;

namespace HxCore.Extensions.Document
{
    public class DocumentHelper
    {
        private const string MemberXPath = "/doc/members/member[@name='{0}']";
        private const string SummaryTag = "summary";
        /// <summary>
        /// 获取控制器action的信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static List<ModuleCreateModel> GetActionDescription(string path, IActionDescriptorCollectionProvider actionDescriptor)
        {
            if (!File.Exists(path)) return new List<ModuleCreateModel>();
            XPathDocument xmlDoc = new XPathDocument(path);
            XPathNavigator _xmlNavigator = xmlDoc.CreateNavigator();
            var actionNamesAndTypes = actionDescriptor.ActionDescriptors.Items
                .Cast<ControllerActionDescriptor>()
                .SkipWhile(actionDesc => actionDesc == null)
                .Select(actionDesc => new ModuleCreateModel
                {
                    RouteUrl = actionDesc.AttributeRouteInfo.Template,
                    Controller = actionDesc.ControllerName,
                    Action = actionDesc.ActionName,
                    MethodInfo = actionDesc.MethodInfo
                });
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
            return actionNamesAndTypes.ToList();
        }
    }
}
