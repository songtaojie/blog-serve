using Hx.Sdk.Attributes;
using Hx.Sdk.Core;
using Hx.Sdk.Entity.Page;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Extras.SqlSugar.Repositories;
using HxCore.IServices;
using HxCore.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services
{
    /// <summary>
    /// 博客查询
    /// </summary>
    public class BlogQuery : BaseQuery<T_Blog>, IBlogQuery
    {
        private readonly IWebManager _webManager;
        public BlogQuery(ISqlSugarRepository<T_Blog> repository, IWebManager webManager) :base(repository)
        {
            _webManager = webManager;
        }
        [CacheData(AbsoluteExpiration = 5)]
        public async Task<SqlSugarPageModel<BlogQueryModel>> GetBlogsAsync(BlogQueryParam param)
        {
            var result = await this.Repository.Entities
                .InnerJoin(this.Db.Queryable<T_User>(),(b,u) =>b.CreaterId == u.Id)
                .Where((b,u) => b.Publish == ConstKey.Yes && b.Deleted == ConstKey.No)
                .OrderBy((b, u) => b.PublishDate, OrderByType.Desc)
                .Select((b, u) => new BlogQueryModel
                {
                    Id = b.Id.ToString(),
                    NickName = u.NickName,
                    UserName = u.UserName,
                    Title = b.Title,
                    PureContent = b.PureContent,
                    ReadCount = b.ReadCount,
                    CmtCount = b.CmtCount,
                    PublishDate = b.PublishDate,
                    CoverImgUrl = b.CoverImgUrl,
                    AvatarUrl = u.AvatarUrl,
                    MarkDown = b.MarkDown
                })
                .ToPagedListAsync(param.PageIndex,param.PageSize);
            return result;
        }
    }
}
