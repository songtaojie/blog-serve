using HxCore.Entity.Context;
using HxCore.Entity.Entities.Ids4;

namespace HxCore.IServices.Ids4
{
    /// <summary>
    /// IDS4用户管理接口
    /// </summary>
    public interface IIds4UserService: IBaseService<ApplicationUser, IdsDbContextLocator>
    {
    }
}
