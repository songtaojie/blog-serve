using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Entity.Entities.Ids4
{
    /// <summary>
    /// 以下model 来自ids4项目，多库模式，为了调取ids4数据
    /// 用户角色表
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole<string>, IEntity<IdsDbContextLocator>
    {
    }
}
