using Elasticsearch.Net;
using HxCore.IServices.Elastic;
using HxCore.Options;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services.Elastic
{
    public class ElasticClientProvider : IElasticClientProvider
    {
        private readonly ElasticSettingsOptions _EsConfig;

        private ConcurrentDictionary<string, ElasticClient> _ElasticLinqClients;

        private ConcurrentDictionary<string, ElasticLowLevelClient> _ElasticJsonClients;

        public ElasticClientProvider(IOptions<ElasticSettingsOptions> esConfig)
        {
            _EsConfig = esConfig.Value;
            _ElasticLinqClients = new ConcurrentDictionary<string, ElasticClient>();
            _ElasticJsonClients = new ConcurrentDictionary<string, ElasticLowLevelClient>();
        }

        public ElasticClient GetElasticLinqClient(string defaultIndex = "")
        {
            if (!_EsConfig.Urls.Any()) throw new Exception("ElasticSettings Urls configuration is missing");
            var key = string.IsNullOrEmpty(defaultIndex) ? "default" : defaultIndex;
            return _ElasticLinqClients.GetOrAdd(key, key => 
            {
                var uris = _EsConfig.Urls.Select(p => new Uri(p)).ToArray();
                var connectionPool = new StaticConnectionPool(uris);
                var connectionSetting = new ConnectionSettings(connectionPool);
                //var connectionSetting = new ConnectionSettings(uris.FirstOrDefault());
                connectionSetting.RequestTimeout(TimeSpan.FromSeconds(30));
                if (!string.IsNullOrWhiteSpace(defaultIndex)) connectionSetting.DefaultIndex(defaultIndex);
                return new ElasticClient(connectionSetting);
            });
        }


        public ElasticLowLevelClient GetElasticJsonClient(string defaultIndex = "")
        {
            if (!_EsConfig.Urls.Any()) throw new Exception("ElasticSettings Urls configuration is missing");
            var key = string.IsNullOrEmpty(defaultIndex) ? "default" : defaultIndex;
            return _ElasticJsonClients.GetOrAdd(key, Function);
            // 本地函数
            ElasticLowLevelClient Function(string key)
            {
                var uris = _EsConfig.Urls.Select(p => new Uri(p)).ToArray();
                //var connectionPool = new StaticConnectionPool(uris);
                var connectionSetting = new ConnectionSettings(uris.FirstOrDefault());
                connectionSetting.RequestTimeout(TimeSpan.FromSeconds(30));
                if (!string.IsNullOrWhiteSpace(defaultIndex)) connectionSetting.DefaultIndex(defaultIndex);
                return new ElasticLowLevelClient(connectionSetting);
            }
        }

    }
}
