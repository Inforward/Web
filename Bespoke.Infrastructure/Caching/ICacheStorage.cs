using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bespoke.Infrastructure.Caching
{
    public interface ICacheStorage
    {
        void Remove(string key);
        void Store(string key, object data);
        void Store(string key, object data, int cacheDurationMinutes);
        T Retrieve<T>(string storageKey);
        T Retrieve<T>(string storageKey, Func<T> fetchMethod);
        T Retrieve<T>(string storageKey, int cacheDurationMinutes, Func<T> fetchMethod);
    }
}
