using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Glz.Infrastructure.Caching
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly ICacheClient _cacheClient;

        public RedisCacheProvider()
        {
            var serializer = new NewtonsoftSerializer();
            _cacheClient = new StackExchangeRedisCacheClient(serializer);
        }

        public void Add(string key, string valKey, object value)
        {
            _cacheClient.HashSet(key, valKey, value);
        }

        public void Put(string key, string valKey, object value)
        {
            Add(key, valKey, value);
        }

        public object Get(string key, string valKey)
        {
            return Exists(key, valKey) ? _cacheClient.HashGet<object>(key, valKey) : null;
        }

        public void Remove(string key)
        {
            _cacheClient.Remove(key);
        }

        public bool Exists(string key)
        {
            return _cacheClient.Exists(key);
        }

        public bool Exists(string key, string valKey)
        {
            return _cacheClient.HashExists(key, valKey);
        }

        public object Get(string key)
        {
            return _cacheClient.Get<object>(key);
        }

        public T Get<T>(string key) where T : class
        {
            return _cacheClient.Get<T>(key);
        }

        public void Set(string key, object value, TimeSpan? slidingExpireTime = default(TimeSpan?))
        {
            if (slidingExpireTime.HasValue)
            {
                _cacheClient.Add(key, value, slidingExpireTime.Value);
            }
            else
            {
                _cacheClient.Add(key, value);
            }
        }

        public void Dispose()
        {
            _cacheClient.Dispose();
        }
    }
}
