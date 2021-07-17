using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.Entity;
using Hx.Sdk.Entity.Page;
using Hx.Sdk.Extensions;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.IServices.Admin;
using HxCore.Model.Admin.Module;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Services.Admin
{
    public class ModuleService : BaseStatusService<T_Module>, IModuleService
    {
        public ModuleService(IRepository<T_Module> userDal) : base(userDal)
        {
        }

        #region 新增编辑
        /// <inheritdoc cref="HxCore.IServices.IModuleService.AddAsync(ModuleCreateModel)"/>
        public async Task<bool> AddAsync(ModuleCreateModel createModel)
        {
            var entity = this.Mapper.Map<T_Module>(createModel);
            var disabled = createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes;
            entity.SetDisable(disabled, UserContext.UserId, UserContext.UserName);
            return await this.InsertAsync(entity);
        }

        /// <inheritdoc cref="HxCore.IServices.IModuleService.InsertAsync(ModuleCreateModel)"/>
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

