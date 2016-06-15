using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure.Caching
{
    public class RedisCache : ICache
    {
        public void Dispose()
        {

        }

        public object Get(string key)
        {
            return CacheManager.Instance.Get(key);
        }

        public T Get<T>(string key) where T : class
        {
            return CacheManager.Instance.Get<T>(key);
        }

        public void Remove(string key)
        {
            CacheManager.Instance.Remove(key);
        }

        public void Set(string key, object value, TimeSpan? slidingExpireTime = default(TimeSpan?))
        {
            CacheManager.Instance.Set(key, value, slidingExpireTime);
        }
    }
}
