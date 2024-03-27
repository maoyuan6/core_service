using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Cache
{
    public class CacheService : ICacheService
    { 
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        public readonly IDatabase _redisDatabase;
        public CacheService(ConnectionMultiplexer connectionMultiplexer)
        { 
            _connectionMultiplexer = connectionMultiplexer;
            _redisDatabase = _connectionMultiplexer.GetDatabase();
        }
        public string GetCacheByKey(string key)
        { 
            return _redisDatabase.StringGet(key).ToString();
        }

        public bool SetCache(object arg, string key)
        { 
            return _redisDatabase.StringSet(key, arg.ToJson()); 
        }
    }
}
