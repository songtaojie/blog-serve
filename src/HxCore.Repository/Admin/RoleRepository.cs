using HxCore.Entity.Entities;
using HxCore.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HxCore.Repository
{
    public class RoleRepository : BaseRepository<T_Role>, IRoleRepository
    {
        public RoleRepository(DbContext db) : base(db)
        { }
    }
}
