using Elasticsearch.Net;
using HxCore.IServices.Elastic;
using HxCore.Options;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services.Elastic
{
    public class ElasticClientProvider : IElasticClientProvider
    {
        private readonly ElasticSettingsOptions _EsConfig;
        public ElasticClientProvider(IOptions<ElasticSettingsOptions> esConfig)
        {
            _EsConfig = esConfig.Value;
        }

        public ElasticClient GetElasticLinqClient(string defaultIndex = "")
        {
            var urls = _EsConfig.Urls;
            if (!urls.Any()) throw new Exception("Elastic urls can not be null");
            var uris = urls.Select(p => new Uri(p)).ToArray();
            //var connectionPool = new SniffingConnectionPool(uris);
            var connectionSetting = new ConnectionSettings(uris.FirstOrDefault());
            connectionSetting.RequestTimeout(TimeSpan.FromSeconds(30));
            if (!string.IsNullOrWhiteSpace(defaultIndex)) connectionSetting.DefaultIndex(defaultIndex);
            return new ElasticClient(connectionSetting);
        }


        public ElasticLowLevelClient GetElasticJsonClient(string defaultIndex = "")
        {
            var urls = _EsConfig.Urls;
            if (!urls.Any()) throw new Exception("Elastic urls can not be null");
            var uris = urls.Select(p => new Uri(p)).ToArray();

            var connectionSetting = new ConnectionSettings(new SniffingConnectionPool(uris));
            connectionSetting.RequestTimeout(TimeSpan.FromSeconds(30));
            if (!string.IsNullOrWhiteSpace(defaultIndex)) connectionSetting.DefaultIndex(defaultIndex);
            return new ElasticLowLevelClient(connectionSetting);
        }

    }
}
