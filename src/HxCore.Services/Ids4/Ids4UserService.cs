using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Context;
using HxCore.Entity.Entities;
using HxCore.IServices.Ids4;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Services.Ids4
{
    public class Ids4UserService: Internal.PrivateService<ApplicationUser, IdsDbContextLocator>, IIds4UserService
    {
        public Ids4UserService(IRepository<ApplicationUser, IdsDbContextLocator> userDal) : base(userDal)
        {
        }
    }
}
