using Hx.Sdk.Common.Extensions;
using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using Hx.Sdk.DependencyInjection;
using Hx.Sdk.FriendlyException;
using HxCore.Entity;
using HxCore.Enums;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HxCore.Services
{
    /// <summary>
    /// 基础的实现类，带状态
    /// 使用默认的数据库上下文定位器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseStatusService<T> : BaseStatusService<T, MasterDbContextLocator>
        where T : Hx.Sdk.DatabaseAccessor.StatusEntityBase, new()
    {
        public BaseStatusService(IRepository<T, MasterDbContextLocator> repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// 基础的实现类，带状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public abstract class BaseStatusService<T, TDbContextLocator> : HxCore.Services.Internal.PrivateService<T, TDbContextLocator>, IScopedDependency
        where T : Hx.Sdk.DatabaseAccessor.StatusEntityBase<string, TDbContextLocator>, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        public BaseStatusService(IRepository<T, TDbContextLocator> repository):base(repository)
        {
        }

        public virtual T BeforeDelete(T entity)
        {
            entity.SetDelete(UserId, UserName);
            return entity;
        }

        public async Task<bool> FakeDeleteAsync(object id)
        {
            var entity = await this.FindAsync(id);
            if (entity == null) throw new UserFriendlyException("未查询到数据",ErrorCodeEnum.DataNull);
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
                entity.SetCreater(UserId, UserName);
            }
            return entity;
        }

        public override T BeforeUpdate(T entity)
        {
            if (entity != null)
            {
                entity.SetModifier(UserId, UserName);
            }
            return entity;
        }
        #endregion
    }
}
