using HxCore.IRepository;
using Microsoft.EntityFrameworkCore;
using HxCore.Entity.Entities;

namespace HxCore.Repository
{
    public class UserInfoRepository:BaseRepository<T_UserInfo>,IUserInfoRepository
    {
        public UserInfoRepository(DbContext dbSession) : base(dbSession)
        { }
    }
}
