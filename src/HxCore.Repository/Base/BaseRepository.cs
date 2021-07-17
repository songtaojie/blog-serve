using Hx.Sdk.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace HxCore.Repository
{
    public abstract class BaseRepository<T>where T:class,new()
    {
        protected DbContext Db { get; }
        public BaseRepository(DbContext db)
        {
            this.Db = db;
        }
        #region 查询
        /// <inheritdoc cref="HxCore.IRepository.IBaseRepository{T}.FirstOrDefaultAsync(Expression{Func{T, bool}})"/>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Db.Set<T>().FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc cref="HxCore.IRepository.IBaseRepository{T}.FindAsync(object)"/>
        public async Task<T> FindAsync(object id)
        {
            return await Db.Set<T>().FindAsync(id);
        }

        /// <inheritdoc cref="HxCore.IRepository.IBaseRepository{T}.QueryEntities(Expression{Func{T, bool}})"/>
        public IQueryable<T> QueryEntities(Expression<Func<T, bool>> lambda)
        {
            var result = Db.Set<T>().Where(lambda);
            return result;
        }
        #endregion

        #region 新增
        /// <inheritdoc cref="HxCore.IRepository.IBaseRepository{T}.InsertAsync(T)"/>
        public async Task<T> InsertAsync(T entity)
        {
            var result= await this.Db.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        /// <inheritdoc cref="HxCore.IRepository.IBaseRepository{T}.BatchInsertAsync(IEnumerable{T})"/>
        public async Task BatchInsertAsync(IEnumerable<T> entityList)
        {
            if (entityList != null && entityList.Count() > 0)
            {
                await this.Db.Set<T>().AddRangeAsync(entityList);
            }
        }
        #endregion

        #region 更新

        /// <inheritdoc cref="HxCore.IRepository.IBaseRepository{T}.UpdateAsync(T)"/>
        public async Task<T> UpdateAsync(T entity)
        {
            return await Task.Run(() =>
            {
                var result = Db.Set<T>().Update(entity);
                return result.Entity;
            });
        }

        /// <inheritdoc cref="HxCore.IRepository.IBaseRepository{T}.BatchUpdateAsync(T[])"/>
        public async Task BatchUpdateAsync(params T[] entitys)
        {
            if (entitys != null && entitys.Length > 0)
            {
                await Task.Run(() =>
                {
                    Db.Set<T>().UpdateRange(entitys);
                });
            }
        }

        /// <inheritdoc cref="HxCore.IRepository.IBaseRepository{T}.UpdatePartialAsync(T, string[])"/>
        public async Task UpdatePartialAsync(T entity, params string[] fields)
        {
            if (entity != null && fields != null)
            {
                await Task.Run(() =>
                {
                    this.Db.Set<T>().Attach(entity);
                    foreach (var item in fields)
                    {
                        this.Db.Entry<T>(entity).Property(item).IsModified = true;
                    }
                });
            }
        }
        #endregion


        #region 删除
        /// <summary>
        /// 删除某个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> RemoveAsync(T entity)
        {
            return await Task.Run(() =>
            {
                return this.Db.Remove(entity).Entity;
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> BatchRemoveAsync(IEnumerable<T> entitys)
        {
            return await Task.Run(() =>
            {
                this.Db.RemoveRange(entitys);
                return true;
            });
        }
        
        /// <summary>
        /// 获取当前执行实体的表名
        /// </summary>
        /// <returns></returns>
        private string GetClassName()
        {
            var type = typeof(T);
            var entityType = Db.Model.FindEntityType(type.FullName);
            if (entityType != null) return entityType.GetTableName();
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null) return tableAttribute.Name;
            return type.Name;
        }
        #endregion

        #region 保存更改
        public async Task<bool> SaveChangesAsync()
        {
            var result = await this.Db.SaveChangesAsync();
            return result > 0;
        }
        #endregion

        #region 判断
        /// <summary>
        /// 判断是否存在满足条件的数据
        /// </summary>
        /// <param name="predicate">lambda表达式</param>
        /// <returns></returns>
        public Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            return this.Db.Set<T>().AnyAsync(predicate);
        }
        #endregion
    }
}
