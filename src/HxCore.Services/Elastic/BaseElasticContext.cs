using Hx.Sdk.FriendlyException;
using Hx.Sdk.UnifyResult;
using HxCore.IServices.Elastic;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services.Elastic
{
    /// <summary>
    /// es操作基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseElasticContext: BaseRESTfulResult
    {
        protected IElasticClientProvider ElasticProvider { get; }
        public abstract string IndexName { get; }
        public BaseElasticContext(IElasticClientProvider esClientProvider)
        {
            ElasticProvider = esClientProvider;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        public async Task<RESTfulResult<bool>> ElasticInsertAsync<TModel>(IEnumerable<TModel> tList) where TModel : class
        {
            var client = ElasticProvider.GetElasticLinqClient(IndexName);
            if (!client.Indices.Exists(IndexName).Exists)
            {
                client.CreateIndex<TModel>(IndexName);
            }
            var response = await client.IndexManyAsync(tList);
            //var response = client.Bulk(p=>p.Index(IndexName).IndexMany(tList));
            //if (!response.IsValid) throw new UserFriendlyException(response.ServerError.Error.Reason,500);
            if (!response.IsValid)
            {
                var message = response.ServerError?.Error?.Reason ?? response.OriginalException?.Message ?? response.DebugInformation;
                return Error<bool>(message);
            }
            return Success(true);
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public async Task<RESTfulResult<long>> ElasticCountAsync<TModel>() where TModel : class
        {
            var client = ElasticProvider.GetElasticLinqClient(IndexName);
            var search = new SearchDescriptor<TModel>().MatchAll(); //指定查询字段 .Source(p => p.Includes(x => x.Field("Id")));
            var response = await client.SearchAsync<TModel>(search);
            if (!response.IsValid)
            {
                var message = response.ServerError?.Error?.Reason ?? response.OriginalException?.Message ?? response.DebugInformation;
                return Error<long>(message);
            }
            return Success(response.Total); 
        }
        /// <summary>
        /// 根据Id删除数据
        /// </summary>
        /// <returns></returns>
        public async Task<RESTfulResult<bool>> ElasticDeleteAsync<TModel>(string id) where TModel : class
        {
            var client = ElasticProvider.GetElasticLinqClient(IndexName);
            var response = await client.DeleteAsync<TModel>(id);
            if (!response.IsValid)
            {
                var message = response.ServerError?.Error?.Reason ?? response.OriginalException?.Message ?? response.DebugInformation;
                return Error<bool>(message);
            }
            return Success(true);
        }

    }
}
