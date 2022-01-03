using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices.Elastic
{
    /// <summary>
    /// ElastciClient提供类
    /// </summary>
    public interface IElasticClientProvider
    {
        /// <summary>
        /// Linq查询的官方Client
        /// </summary>
        /// <param name="defaultIndex">默认索引</param>
        /// <returns></returns>
        ElasticClient GetElasticLinqClient(string defaultIndex = "");

        /// <summary>
        ///  Js查询的官方Client
        /// </summary>
        /// <param name="defaultIndex">默认索引</param>
        /// <returns></returns>
        ElasticLowLevelClient GetElasticJsonClient(string defaultIndex = "");
    }
}
