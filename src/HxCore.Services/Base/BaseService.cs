using Hx.Sdk.Common.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.DependencyInjection;

namespace HxCore.Services
{
    /// <summary>
    /// 基础服务的实现类
    /// 使用默认的数据库上下文定位器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : BaseService<T, MasterDbContextLocator>
        where T : Hx.Sdk.DatabaseAccessor.EntityBase, new()
    {
        public BaseService(IRepository<T, MasterDbContextLocator> repository) : base(repository)
        {
        }
    }
    /// <summary>
    /// 基础服务实现基础类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public abstract class BaseService<T, TDbContextLocator> : Internal.PrivateService<T, TDbContextLocator>, IScopedDependency
        where T:Hx.Sdk.DatabaseAccessor.EntityBase, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        public BaseService(IRepository<T, TDbContextLocator> repository):base(repository)
        {
        }

        #region 虚方法
        public override T BeforeInsert(T entity)
        {
            if (entity != null)
            {
                entity.Id = Helper.GetSnowId();
                if (UserContext!=null && UserContext.IsAuthenticated)
                {
                    entity.SetCreater(UserContext.UserId, UserContext.UserName);
                }
            }
            return entity;
        }

        public override T BeforeUpdate(T entity)
        {
            if (entity != null && UserContext != null && UserContext.IsAuthenticated)
            {
                entity.SetModifier(UserContext.UserId, UserContext.UserName);
            }
            return entity;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityIds">要删除的id的集合</param>
        /// <returns></returns>
        public async Task<bool> BatchRemoveAsync(IEnumerable<string> entityIds)
        {
            var entityList = entityIds.Select(id => new T { Id = id });
            await this.Repository.DeleteAsync(entityList);
            return await this.Repository.SaveNowAsync() > 0;
        }
        #endregion 
    }
}
