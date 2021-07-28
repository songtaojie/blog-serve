using Hx.Sdk.Common.Helper;
using Hx.Sdk.ConfigureOptions;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.Entity;
using Hx.Sdk.Entity.Page;
using Hx.Sdk.Extensions;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.IServices.Admin;
using HxCore.Model.Admin.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Services.Admin
{
    public class ModuleService : BaseStatusService<T_Module>, IModuleService
    {
        private IActionDescriptorCollectionProvider _actionProvider;
        public ModuleService(IRepository<T_Module> userDal, IActionDescriptorCollectionProvider actionProvider) : base(userDal)
        {
            _actionProvider = actionProvider;
        }

        #region 新增编辑
        /// <inheritdoc cref="IModuleService.AddAsync"/>
        public async Task<bool> AddAsync(ModuleCreateModel createModel)
        {
            var entity = this.Mapper.Map<T_Module>(createModel);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserContext.UserId, UserContext.UserName);
            return await this.InsertAsync(entity);
        }

        /// <inheritdoc cref="IModuleService.UpdateAsync"/>
        public async Task<bool> UpdateAsync(ModuleCreateModel createModel)
        {
            if (string.IsNullOrEmpty(createModel.Id)) throw new UserFriendlyException("无效的标识");
            var entity = await this.FindAsync(createModel.Id);
            if(entity == null) throw new UserFriendlyException("未找到接口信息");
            entity = this.Mapper.Map(createModel, entity);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserContext.UserId, UserContext.UserName);
            return await this.UpdateAsync(entity);
        }

        /// <inheritdoc cref="IModuleService.SyncInterface"/>
        public async Task<bool> SyncInterface()
        {
            // 获取所有的控制器和动作方法
            var actionDescs = _actionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>().Select(x => new
            {
                ControllerName = x.ControllerName,
                ActionName = x.ActionName,
                DisplayName = x.DisplayName,
                RouteTemplate = x.AttributeRouteInfo.Template,
                Attributes = x.MethodInfo.CustomAttributes.Select(z => new {
                    TypeName = z.AttributeType.FullName,
                    ConstructorArgs = z.ConstructorArguments.Select(v => new {
                        ArgumentValue = v.Value
                    }),
                    NamedArguments = z.NamedArguments.Select(v => new {
                        v.MemberName,
                        TypedValue = v.TypedValue.Value,
                    }),
                }),
                ActionId = x.Id,
                x.RouteValues,
                Parameters = x.Parameters.Select(z => new {
                    z.Name,
                    TypeName = z.ParameterType.Name,
                })
            });

            if (actionDescs.Any())
            { 
                
            }

            return await Task.FromResult(true);
        }

        #endregion

        #region 查询
        /// <inheritdoc cref="HxCore.IServices.IModuleService.QueryModulePageAsync(ModuleQueryParam)"/>
        public Task<PageModel<ModuleQueryModel>> QueryModulePageAsync(ModuleQueryParam param)
        {
            var query = from m in this.Repository.DetachedEntities
                        where m.Deleted == ConstKey.No
                        orderby m.CreateTime descending
                        select new ModuleQueryModel
                        {
                            Id = m.Id,
                            Name = m.Name,
                            LinkUrl = m.LinkUrl,
                            Description = m.Description,
                            OrderSort = m.OrderSort,
                            CreateTime = m.CreateTime,
                            Creater = m.Creater,
                            IsEnabled = m.Disabled == ConstKey.No
                        };
            return query.ToOrderAndPageListAsync(param);
        }

        /// <inheritdoc cref="HxCore.IServices.IModuleService.GetAsync(string)"/>
        public async Task<ModuleDetailModel> GetAsync(string id)
        {
            var module = await this.FindAsync(id);
            if (module == null) throw new UserFriendlyException("未找到接口信息");
            ModuleDetailModel detailModel = this.Mapper.Map<ModuleDetailModel>(module);
            detailModel.IsEnabled = Helper.IsNo(module.Disabled);
            return detailModel;
        }
        #endregion
    }
}

