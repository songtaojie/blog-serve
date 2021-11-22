using AutoMapper;
using Hx.Sdk.Core;
using Hx.Sdk.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace HxCore.Services.Internal
{
    public abstract class PrivateService<T, TDbContextLocator>
        where T :class, Hx.Sdk.DatabaseAccessor.IEntity<TDbContextLocator>, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 仓储
        /// </summary>
        protected IRepository<T, TDbContextLocator> Repository { get; }
        /// <summary>
        /// 用户上下文
        /// </summary>
        protected IUserContext UserContext { get; }
        /// <summary>
        /// AutoMapper映射对象
        /// </summary>
        protected IMapper Mapper { get; }
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected Microsoft.EntityFrameworkCore.DbContext Db { get; }
        public PrivateService(IRepository<T, TDbContextLocator> repository)
        {
            this.Repository = repository;
            this.Mapper = repository.ServiceProvider.GetRequiredService<IMapper>();
            this.UserContext = repository.ServiceProvider.GetRequiredService<IUserContext>();
            this.Db = this.Repository.Context;
        }
        #region 查询
        /// <inheritdoc cref="HxCore.IServices.IBaseService{T}.FirstOrDefaultAsync(Expression{Func{T, bool}}, bool)"/>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool defaultFilter = true)
        {
            return await Repository.FirstOrDefaultAsync(GetLambda(predicate, defaultFilter));
        }

        /// <inheritdoc cref="HxCore.IServices.IBaseService{T}.FindAsync(object)"/>
        public async Task<T> FindAsync(object id)
        {
            return await Repository.FindAsync(id);
        }

        /// <inheritdoc cref="HxCore.IServices.IBaseService{T}.QueryEntities(Expression{Func{T, bool}}, bool)"/>
        public IQueryable<T> QueryEntities(Expression<Func<T, bool>> predicate, bool defaultFilter = true)
        {
            return Repository.Where(GetLambda(predicate, defaultFilter));
        }

        #region 获取新的lambda
        protected virtual Expression<Func<T, bool>> GetLambda(Expression<Func<T, bool>> lambdaWhere, bool defaultFilter)
        {
            return lambdaWhere;
        }
        #endregion

        #endregion

        #region 新增

        /// <inheritdoc cref="HxCore.IServices.IBaseService{T}.InsertAsync(T)"/>
        public async Task<bool> InsertAsync(T entity)
        {
            entity = this.BeforeInsert(entity);
            await Repository.InsertAsync(entity);
            var result = await Repository.SaveNowAsync();
            return result > 0;
        }

        /// <inheritdoc cref="HxCore.IServices.IBaseService{T}.BatchInsertAsync(IEnumerable{T})"/>
        public async Task<bool> BatchInsertAsync(IEnumerable<T> entityList)
        {
            List<T> newList = new List<T>();
            if (entityList != null && entityList.Count() > 0)
            {
                foreach (var entity in entityList)
                {
                    newList.Add(this.BeforeInsert(entity));
                }
            }
            await Repository.InsertAsync(newList);
            var result = await this.Repository.SaveNowAsync();
            return result > 0;
        }
        #endregion

        #region 更新


        /// <inheritdoc cref="HxCore.IServices.IBaseService{T}.UpdateAsync(T)"/>
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            entity = this.BeforeUpdate(entity);
            await Repository.UpdateAsync(entity);
            var result = await this.Repository.SaveNowAsync();
            return result > 0;
        }

        /// <inheritdoc cref="HxCore.IServices.IBaseService{T}.UpdatePartialAsync(T, string[]))"/>
        public virtual async Task<bool> UpdatePartialAsync(T entity, params string[] fields)
        {
            await Repository.UpdateIncludeAsync(entity, fields);
            var result = await this.Repository.SaveNowAsync();
            return result > 0;
        }
        #endregion

        #region 判断
        /// <summary>
        /// 判断是否存在满足条件的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            return this.Repository.AnyAsync(predicate);
        }
        #endregion

        #region 虚方法
        public virtual T BeforeInsert(T entity)
        {
            return entity;
        }

        public virtual T BeforeUpdate(T entity)
        {
            return entity;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除某个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            await this.Repository.DeleteAsync(id);
            return await this.Repository.SaveNowAsync() > 0;
        }

        /// <summary>
        /// 删除某个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T entity)
        {
            await this.Repository.DeleteAsync(entity);
            return await this.Repository.SaveNowAsync() > 0;
        }
        #endregion 
    }
}
