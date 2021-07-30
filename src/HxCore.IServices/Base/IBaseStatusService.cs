using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 带有状态的基础服务的接口
    /// 使用默认的上下文定位器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseStatusService<T> : IBaseStatusService<T,MasterDbContextLocator> 
        where T : Hx.Sdk.Entity.IEntity
    { 
    }

    /// <summary>
    /// 带有状态的基础服务的接口
    /// </summary>
    /// <typeparam name="T">模型数据</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public interface IBaseStatusService<T, TDbContextLocator> :IBaseService<T, TDbContextLocator>
        where T : Hx.Sdk.Entity.IEntity
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 删除数据，物理删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> FakeDeleteAsync(object id);
    }
}
