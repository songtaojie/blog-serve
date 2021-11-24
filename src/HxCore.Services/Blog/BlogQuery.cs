using Hx.Sdk.Entity.Page;
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
        public BlogQuery(ISqlSugarRepository<T_Blog> repository):base(repository)
        { 
            
        }
        public async Task<SqlSugarPageModel<BlogQueryModel>> QueryBlogsAsync(BlogQueryParam param)
        {
            var result = await this.Repository.Entities.Where(b => true)
                .Select<BlogQueryModel>(b=> new BlogQueryModel
                { 
                    Id = b.Id
                })
                .ToPagedListAsync(param.PageIndex, param.PageSize);
            return result;
        }
    }
}
