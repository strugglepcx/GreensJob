using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure.Caching
{
    public class NullCache : ICache
    {
        public void Dispose()
        {

        }

        public object Get(string key)
        {
            return null;
        }

        public void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {

        }

        public void Remove(string key)
        {

        }

        public T Get<T>(string key) where T : class
        {
            return default(T);
        }
    }
}
