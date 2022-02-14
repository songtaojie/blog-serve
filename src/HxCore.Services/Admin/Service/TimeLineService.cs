using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.FriendlyException;
using HxCore.Entity.Entities;
using HxCore.Enums;
using HxCore.IServices;
using HxCore.Model.Admin.TimeLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public class TimeLineService : BaseStatusService<T_TimeLine>, ITimeLineService
    {
        public TimeLineService(IRepository<T_TimeLine, MasterDbContextLocator> dal) : base(dal)
        {
        }
        #region 新增编辑
        public async Task<bool> InsertAsync(TimeLineManageCreateModel createModel)
        {
            var entity = this.Mapper.Map<T_TimeLine>(createModel);
            this.BeforeInsert(entity);
            entity.SetDisable(createModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserId, UserName);
            await this.Repository.InsertAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TimeLineManageCreateModel updateModel)
        {
            if (string.IsNullOrEmpty(updateModel.Id)) throw new UserFriendlyException("无效的标识", ErrorCodeEnum.ParamsNullError);
            var entity = await this.FindAsync(updateModel.Id);
            if (entity == null) throw new UserFriendlyException("该公告通知不存在", ErrorCodeEnum.DataNull);
            entity = this.Mapper.Map(updateModel, entity);
            entity.SetDisable(updateModel.IsEnabled ? StatusEntityEnum.No : StatusEntityEnum.Yes, UserId, UserName);
            this.BeforeUpdate(entity);
            await this.Repository.UpdateAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }
        #endregion
    }
}
