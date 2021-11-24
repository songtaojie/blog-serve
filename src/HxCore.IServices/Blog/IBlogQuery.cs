using SqlSugar;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    public interface IBlogQuery
    {
        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<Model.BlogQueryModel>> QueryBlogsAsync(Model.BlogQueryParam param);
    }
}
