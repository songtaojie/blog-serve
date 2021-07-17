using Hx.Sdk.Common.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.DependencyInjection;

namespace HxCore.Services
{
    public abstract class BaseService<T>: HxCore.Services.Internal.PrivateService<T>,IScopedDependency
        where T:Hx.Sdk.DatabaseAccessor.EntityBase, new()
    {
        //protected Microsoft.EntityFrameworkCore.DbContext Db { get; }
        public BaseService(IRepository<T> repository):base(repository)
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
