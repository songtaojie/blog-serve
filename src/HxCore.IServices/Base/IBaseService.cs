using Hx.Sdk.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    public interface IBaseService<T> : IBaseService<T, MasterDbContextLocator>
        where T : Hx.Sdk.Entity.IEntity
    { }

    public interface IBaseService<T, TDbContextLocator>
        where T: Hx.Sdk.Entity.IEntity
        where TDbContextLocator : class, IDbContextLocator
    {
        #region 查询
        /// <summary>
        /// 根据Id获取模型数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>模型数据</returns>
        Task<T> FindAsync(object id);

        /// <summary>
        /// 获取满足指定条件的一条数据
        /// </summary>
        /// <param name="predicate">获取数据的条件lambda</param>
        /// <returns>满足当前条件的一个实体</returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool defaultFilter = true);

        /// <summary>
        /// 查询符合条件的集合
        /// </summary>
        /// <param name="predicate">lambda表达式</param>
        /// <returns></returns>
        IQueryable<T> QueryEntities(Expression<Func<T, bool>> predicate, bool defaultFilter = true);

        #endregion

        #region 新增
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        Task<bool> InsertAsync(T entity);
        /// <summary>
        /// 插入集合
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task<bool> BatchInsertAsync(IEnumerable<T> entityList);
        #endregion

        #region 更新
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// 更新实体的部分字段
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fields">要更新的字段的集合</param>
        Task<bool> UpdatePartialAsync(T entity, params string[] fields);
        #endregion

        #region 判断
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
        #endregion

        #region 删除
        /// <summary>
        /// 根据主键删除，不用先查询
        /// </summary>
        /// <param name="id">要删除的主键</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);
        #endregion
    }
}
