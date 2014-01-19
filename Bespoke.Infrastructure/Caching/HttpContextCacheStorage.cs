using System;
using System.Web;
using System.Web.Caching;

namespace Bespoke.Infrastructure.Caching
{
    public class HttpContextCacheStorage : ICacheStorage
    {
        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public void Store(string key, object data)
        {
            Store(key, data, (60 * 20));
        }

        public void Store(string key, object data, int cacheExpirationInSeconds)
        {
            HttpRuntime.Cache.Insert(key, data, null, DateTime.Now.AddSeconds(cacheExpirationInSeconds), Cache.NoSlidingExpiration);
        }

        public T Retrieve<T>(string key)
        {
            return (T)HttpRuntime.Cache.Get(key);
        }

        public T Retrieve<T>(string key, Func<T> fetchMethod)
        {
            var result = Retrieve<T>(key);

            if (result == null)
            {
                result = fetchMethod();

                if (result != null)
                    Store(key, result);
            }

            return result;
        }
    }
}
