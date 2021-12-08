using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.FriendlyException;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.IServices;
using HxCore.Model.Admin.FriendLink;
using System.Threading.Tasks;

namespace HxCore.Services
{
    /// <summary>
    /// 友情链接服务类
    /// </summary>
    public class FriendLinkService : BaseStatusService<T_FriendLink>, IFriendLinkService
    {
        public FriendLinkService(IRepository<T_FriendLink, MasterDbContextLocator> dal) : base(dal)
        {
        }
        #region 新增编辑
        public async Task<bool> InsertAsync(FriendLinkManageCreateModel createModel)
        {
            if(await this.ExistAsync(r=>r.SiteCode == createModel.SiteCode)) throw new UserFriendlyException("网站Code已存在", ErrorCodeEnum.AddError);
            var entity = this.Mapper.Map<T_FriendLink>(createModel);
            this.BeforeInsert(entity);
            entity.SetDisable(createModel.IsEnabled?StatusEntityEnum.No: StatusEntityEnum.Yes, UserContext.UserId, UserContext.UserName);
            await this.Repository.InsertAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }

        public async Task<bool> UpdateAsync(FriendLinkManageCreateModel updateModel)
        {
            if (string.IsNullOrEmpty(updateModel.Id)) throw new UserFriendlyException("无效的标识", ErrorCodeEnum.ParamsNullError);
            if (await this.ExistAsync(r => r.SiteCode == updateModel.SiteCode && r.Id != updateModel.Id)) throw new UserFriendlyException("网站Code已存在", ErrorCodeEnum.AddError);
            var entity = await this.FindAsync(updateModel.Id);
            if (entity == null) throw new UserFriendlyException("该友情链接不存在", ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(updateModel, entity);
            entity.SetDisable(updateModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserContext.UserId, UserContext.UserName);
            this.BeforeUpdate(entity);
            await this.Repository.UpdateAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }
        #endregion

    }
}
