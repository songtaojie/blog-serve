using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Extras.SqlSugar
{
    public class BaseQuery : BaseQueryDbContext, IBaseQuery
    {
        public BaseQuery(ConnectionConfig connectionConfig) : base(connectionConfig)
        {
        }

        public IAdo Ado => Db.Ado;
    }
}
