using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.FriendlyException;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.IServices;
using HxCore.Model.Admin.Banner;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class BannerService : BaseStatusService<T_BannerInfo>, IBannerService
    {
        public BannerService(IRepository<T_BannerInfo, MasterDbContextLocator> dal) : base(dal)
        {
        }
        #region 新增编辑
        public async Task<bool> InsertAsync(BannerManageCreateModel createModel)
        {
            if (await this.ExistAsync(r => r.Title == createModel.Title)) throw new UserFriendlyException("该标题已存在", ErrorCodeEnum.AddError);
            var entity = this.Mapper.Map<T_BannerInfo>(createModel);
            this.BeforeInsert(entity);
            entity.SetDisable(createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserContext.UserId, UserContext.UserName);
            await this.Repository.InsertAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BannerManageCreateModel updateModel)
        {
            if (string.IsNullOrEmpty(updateModel.Id)) throw new UserFriendlyException("无效的标识", ErrorCodeEnum.ParamsNullError);
            if (await this.ExistAsync(r => r.Title == updateModel.Title && r.Id != updateModel.Id)) throw new UserFriendlyException("该标题已存在", ErrorCodeEnum.AddError);
            var entity = await this.FindAsync(updateModel.Id);
            if (entity == null) throw new UserFriendlyException("该公告通知不存在", ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(updateModel, entity);
            entity.SetDisable(updateModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserContext.UserId, UserContext.UserName);
            this.BeforeUpdate(entity);
            await this.Repository.UpdateAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }
        #endregion
    }
}
