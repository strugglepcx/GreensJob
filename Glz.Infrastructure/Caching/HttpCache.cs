using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Glz.Infrastructure.Caching
{
    /// <summary>
    /// IIS缓存实现
    /// </summary>
    public class HttpCache : ICache
    {
        public void Dispose()
        {

        }

        public object Get(string key)
        {
            return HttpContext.Current == null ? null : HttpContext.Current.Cache.Get(key);
        }

        public void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            if (HttpContext.Current == null) return;
            if (slidingExpireTime.HasValue)
            {

                HttpContext.Current.Cache.Insert(key, value, null,
                    DateTime.MaxValue, slidingExpireTime.Value);
            }
            else
            {
                HttpContext.Current.Cache.Insert(key, value);
            }
        }

        public void Remove(string key)
        {
            if (HttpContext.Current == null) return;
            HttpContext.Current.Cache.Remove(key);
        }

        public T Get<T>(string key) where T : class
        {
            return HttpContext.Current == null ? default(T) : HttpContext.Current.Cache.Get(key) as T;
        }
    }
}
