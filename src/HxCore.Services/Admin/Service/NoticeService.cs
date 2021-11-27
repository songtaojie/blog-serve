using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.FriendlyException;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.IServices;
using HxCore.Model.Admin.Notice;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class NoticeService : BaseStatusService<T_NoticeInfo>, INoticeService
    {
        public NoticeService(IRepository<T_NoticeInfo, MasterDbContextLocator> dal) : base(dal)
        {
        }
        #region 新增编辑
        public async Task<bool> InsertAsync(NoticeCreateModel createModel)
        {
            var entity = this.Mapper.Map<T_NoticeInfo>(createModel);
            this.BeforeInsert(entity);
            entity.SetDisable(createModel.IsEnabled ? StatusEntityEnum.Yes : StatusEntityEnum.No, UserContext.UserId, UserContext.UserName);
            await this.Repository.InsertAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }

        public async Task<bool> UpdateAsync(NoticeCreateModel updateModel)
        {
            if (string.IsNullOrEmpty(updateModel.Id)) throw new UserFriendlyException("无效的标识", ErrorCodeEnum.ParamsNullError);
            var entity = await this.FindAsync(updateModel.Id);
            if (entity == null) throw new UserFriendlyException("该公告通知不存在", ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(updateModel, entity);
            entity.SetDisable(updateModel.IsEnabled ? StatusEntityEnum.Yes : StatusEntityEnum.No, UserContext.UserId, UserContext.UserName);
            this.BeforeUpdate(entity);
            await this.Repository.UpdateAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }
        #endregion
    }
}
