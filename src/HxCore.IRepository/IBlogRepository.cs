using HxCore.Entity.Entities;

namespace HxCore.IRepository
{
    public interface IBlogRepository:IBaseRepository<T_Blog>
    {
    }

    public interface IBlogTagRepository : IBaseRepository<T_BlogTag>
    {
    }
}
