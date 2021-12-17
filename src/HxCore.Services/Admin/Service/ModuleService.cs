using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.Entity.Page;
using Hx.Sdk.Extensions;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Model.Admin.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hx.Sdk.FriendlyException;
using HxCore.Enums;
using HxCore.IServices;

namespace HxCore.Services
{
    public class ModuleService : BaseStatusService<T_Module>, IModuleService
    {
        public ModuleService(IRepository<T_Module,MasterDbContextLocator> userDal) : base(userDal)
        {
        }

        #region 新增编辑
        /// <inheritdoc cref="IModuleService.AddAsync"/>
        public async Task<bool> AddAsync(ModuleCreateModel createModel)
        {
            var entity = this.Mapper.Map<T_Module>(createModel);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserId, UserName);
            return await this.InsertAsync(entity);
        }

        /// <inheritdoc cref="IModuleService.BatchAddOrUpdateAsync"/>
        public async Task<bool> BatchAddOrUpdateAsync(List<ModuleCreateModel> createModels)
        {
            List<T_Module> addEntitys = new List<T_Module>();
            List<T_Module> updateEntitys = new List<T_Module>();
            var names = createModels.Select(m => m.Name).ToArray();
            var modules = this.Repository.Where(m => names.Contains(m.Name)).ToList();
            createModels.ForEach(cm =>
            {
                var module = modules.FirstOrDefault(m => m.Name == cm.Name);
                if (module == null)
                {
                    module = this.Mapper.Map<T_Module>(cm);
                    this.BeforeInsert(module);
                    module.SetDisable(StatusEntityEnum.No, UserId, UserName);
                    addEntitys.Add(module);
                }
                else
                {
                    module = this.Mapper.Map(cm, module);
                    this.BeforeUpdate(module);
                    updateEntitys.Add(module);
                }
            });
            if(addEntitys.Any()) await Repository.InsertAsync(addEntitys);
            if (updateEntitys.Any()) await Repository.UpdateAsync(updateEntitys);
            return await this.Repository.SaveNowAsync() > 0;
        }

        /// <inheritdoc cref="IModuleService.UpdateAsync"/>
        public async Task<bool> UpdateAsync(ModuleUpdateModel createModel)
        {
            if (string.IsNullOrEmpty(createModel.Id)) throw new UserFriendlyException("无效的标识",ErrorCodeEnum.ParamsNullError);
            var entity = await this.FindAsync(createModel.Id);
            if(entity == null) throw new UserFriendlyException("未找到接口信息", ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(createModel, entity);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserId, UserName);
            return await this.UpdateAsync(entity);
        }
        #endregion

        #region 查询
        /// <inheritdoc cref="IModuleService.QueryModulePageAsync(ModuleQueryParam)"/>
        public Task<PageModel<ModuleQueryModel>> QueryModulePageAsync(ModuleQueryParam param)
        {
            var query = from m in this.Repository.DetachedEntities
                        where m.Deleted == ConstKey.No
                        orderby m.CreateTime descending
                        select new ModuleQueryModel
                        {
                            Id = m.Id,
                            Name = m.Name,
                            RouteUrl = m.RouteUrl,
                            Description = m.Description,
                            OrderSort = m.OrderSort,
                            CreateTime = m.CreateTime,
                            Creater = m.Creater,
                            IsEnabled = m.Disabled == ConstKey.No
                        };
            return query.ToOrderAndPageListAsync(param);
        }

        /// <inheritdoc cref="IModuleService.GetAsync(string)"/>
        public async Task<ModuleDetailModel> GetAsync(string id)
        {
            var module = await this.FindAsync(id);
            if (module == null) throw new UserFriendlyException("未找到接口信息", ErrorCodeEnum.DataNull);
            ModuleDetailModel detailModel = this.Mapper.Map<ModuleDetailModel>(module);
            detailModel.IsEnabled = Helper.IsNo(module.Disabled);
            return detailModel;
        }
        /// <inheritdoc cref="IModuleService.GetListAsync"/>
        public Task<List<ModuleQueryModel>> GetListAsync(ModuleQueryParam param)
        {
            var query = from m in this.Repository.DetachedEntities
                        where m.Deleted == ConstKey.No
                        orderby m.CreateTime descending
                        select new ModuleQueryModel
                        {
                            Id = m.Id,
                            Name = m.Name,
                            RouteUrl = m.RouteUrl,
                            Description = m.Description,
                            OrderSort = m.OrderSort,
                            CreateTime = m.CreateTime,
                            Creater = m.Creater,
                            IsEnabled = m.Disabled == ConstKey.No
                        };
            if (!string.IsNullOrEmpty(param.Name))
            {
                query = query.Where(m => m.Name.Contains(param.Name) || m.Description.Contains(param.Name));
            }
            return query.Take(50).ToListAsync();
        }
        #endregion
    }
}

