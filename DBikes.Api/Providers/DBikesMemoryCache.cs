using Microsoft.Extensions.Caching.Memory;
using System;
namespace DBikes.Api.Providers
{
    public class DBikesMemoryCache
    {
        private MemoryCache cache { get; set; }
        private int cacheLifetime;

        public DBikesMemoryCache(int defaultLifetime)
        {
            cacheLifetime = defaultLifetime;
            MemoryCacheOptions memCacheOptions = new MemoryCacheOptions();
            cache = new MemoryCache(memCacheOptions);
        }

        public void AddToCache(string key, Object o)
        {
            MemoryCacheEntryOptions itemOptions = new MemoryCacheEntryOptions();
            itemOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheLifetime);
            cache.Set(key, o,itemOptions);
        }

        public object CheckCache(string key)
        {
            var o = cache.Get(key);
            return o;
        }

    }
}