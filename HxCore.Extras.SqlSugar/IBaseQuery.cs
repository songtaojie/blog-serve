using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Extras.SqlSugar
{
    public interface IBaseQuery
    {
        IAdo Ado { get; }
        ISqlSugarClient Db { get; }
    }
}
