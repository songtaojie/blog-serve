using Hx.Sdk.Common.Extensions;
using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.DependencyInjection;
using HxCore.Entity;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HxCore.Services
{
    public abstract class BaseStatusService<T> : HxCore.Services.Internal.PrivateService<T>, IScopedDependency
        where T : Hx.Sdk.DatabaseAccessor.StatusEntityBase, new()
    {
        public BaseStatusService(IRepository<T> repository):base(repository)
        {
        }

        public virtual T BeforeDelete(T entity)
        {
            if (entity != null && UserContext != null && UserContext.IsAuthenticated)
            {
                entity.SetDelete(UserContext.UserId, UserContext.UserName);
            }
            return entity;
        }

        public async Task<bool> FakeDeleteAsync(object id)
        {
            var entity = await this.FindAsync(id);
            if (entity == null) throw new Hx.Sdk.Entity.UserFriendlyException("未查询到数据");
            entity = this.BeforeDelete(entity);
            await this.Repository.UpdateIncludeAsync(entity, 
                new string[] { nameof(entity.Deleted), nameof(entity.LastModifierId), 
                    nameof(entity.LastModifier),  nameof(entity.LastModifyTime)});
            return await this.Repository.SaveNowAsync() > 0;
        }

        #region 重写查询过滤
        protected override Expression<Func<T, bool>> GetLambda(Expression<Func<T, bool>> lambdaWhere, bool defaultFilter)
        {
            if (defaultFilter)
            {
                var deleteName = nameof(StatusEntityBase.Deleted);
                ParameterExpression parameterExp = Expression.Parameter(typeof(T), "table");
                MemberExpression deleteProp = Expression.Property(parameterExp, deleteName);
                var lambda = Expression.Lambda<Func<T, bool>>(Expression.Equal(deleteProp, Expression.Constant(ConstKey.No)), parameterExp);
                return lambdaWhere.And(lambda);
            }
            return lambdaWhere;
        }
        #endregion

        #region 虚方法
        public override T BeforeInsert(T entity)
        {
            if (entity != null)
            {
                entity.Id = Helper.GetSnowId();
                if (UserContext != null && UserContext.IsAuthenticated)
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
    }
}
