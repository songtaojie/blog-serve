using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.DependencyInjection;
using HxCore.Entity.Entities.Ids4;
using HxCore.IServices.Ids4;

namespace HxCore.Services.Ids4
{
    public class Ids4UserService: Internal.PrivateService<ApplicationUser, IdsDbContextLocator>, IIds4UserService, IScopedDependency
    {
        public Ids4UserService(IRepository<ApplicationUser, IdsDbContextLocator> userDal) : base(userDal)
        {
        }
    }
}
