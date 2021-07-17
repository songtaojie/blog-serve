using HxCore.Entity.Entities;
using HxCore.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HxCore.Repository.Admin
{
    public class PermissionRepository : BaseRepository<T_Menu>, IPermissionRepository
    {
        public PermissionRepository(DbContext db) : base(db)
        { }
    }
}
