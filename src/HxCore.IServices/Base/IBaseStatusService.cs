using Hx.Sdk.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    public interface IBaseStatusService<T>:IBaseService<T> where T : Hx.Sdk.Entity.IEntity
    {
        /// <summary>
        /// 删除数据，物理删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> FakeDeleteAsync(object id);
    }
}
