using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Extras.SqlSugar.Repositories
{
    /// <summary>
    /// 非泛型 SqlSugar 仓储
    /// </summary>
    public partial class SqlSugarRepository : ISqlSugarRepository
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="db"></param>
        public SqlSugarRepository(IServiceProvider serviceProvider, ISqlSugarClient db)
        {
            _serviceProvider = serviceProvider;
            Ado = db.Ado;
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public virtual SqlSugarClient Context { get; }

        /// <summary>
        /// 原生 Ado 对象
        /// </summary>
        public virtual IAdo Ado { get; }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        public virtual ISqlSugarRepository<TEntity> Change<TEntity>()
            where TEntity : class, new()
        {
            return _serviceProvider.GetService(typeof(ISqlSugarRepository<TEntity>)) as ISqlSugarRepository<TEntity>;
        }
    }

    /// <summary>
    /// SqlSugar 仓储实现类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class SqlSugarRepository<TEntity> : ISqlSugarRepository<TEntity>
    where TEntity : class, new()
    {
        /// <summary>
        /// 非泛型 SqlSugar 仓储
        /// </summary>
        private readonly ISqlSugarRepository _sqlSugarRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlSugarRepository"></param>
        public SqlSugarRepository(ISqlSugarRepository sqlSugarRepository)
        {
            _sqlSugarRepository = sqlSugarRepository;

            Ado = sqlSugarRepository.Ado;
        }

        /// <summary>
        /// 实体集合
        /// </summary>
        public virtual ISugarQueryable<TEntity> Entities => _sqlSugarRepository.Context.Queryable<TEntity>();

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public virtual SqlSugarClient Context { get; }

        /// <summary>
        /// 原生 Ado 对象
        /// </summary>
        public virtual IAdo Ado { get; }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Entities.CountAsync(whereExpression);
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Entities.AnyAsync(whereExpression);
        }

        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TEntity Single(object Id)
        {
            return Entities.InSingle(Id);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Entities.SingleAsync(whereExpression);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Entities.FirstAsync(whereExpression);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<TEntity> ToList()
        {
            return Entities.ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public List<TEntity> ToList(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Entities.Where(whereExpression).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public List<TEntity> ToList(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return Entities.OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public Task<List<TEntity>> ToListAsync()
        {
            return Entities.ToListAsync();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Entities.Where(whereExpression).ToListAsync();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return Entities.OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression).ToListAsync();
        }


        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TChangeEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        public virtual ISqlSugarRepository<TChangeEntity> Change<TChangeEntity>()
            where TChangeEntity : class, new()
        {
            return _sqlSugarRepository.Change<TChangeEntity>();
        }
    }
}
