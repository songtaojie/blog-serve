using HxCore.Entity.Entities;
using HxCore.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HxCore.Repository.Admin
{
    public class ModuleRepository : BaseRepository<T_Module>, IModuleRepository
    {
        public ModuleRepository(DbContext db) : base(db)
        { }
    }
}
