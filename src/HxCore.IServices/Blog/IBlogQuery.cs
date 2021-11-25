using HxCore.Model;
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
        Task<SqlSugarPageModel<BlogQueryModel>> GetBlogsAsync(Model.BlogQueryParam param);


        Task<BlogDetailModel> FindById(string id);
    }
}
