using Hx.Sdk.Entity.Page;
using HxCore.Entity.Context;
using HxCore.Entity.Entities.Ids4;
using HxCore.Model.Admin.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HxCore.IServices.Ids4
{
    /// <summary>
    /// IDS4用户管理接口
    /// </summary>
    public interface IIds4UserService: IBaseService<ApplicationUser, IdsDbContextLocator>
    {
    }
}
