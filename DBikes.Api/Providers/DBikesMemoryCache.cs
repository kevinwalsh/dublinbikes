using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using DBikes.Api.Models;
using System.Runtime.Caching;

namespace DBikes.Api.Providers
{
    public class DBikesMemoryCache
    {
        private ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy cacheItemPolicy;

        public DBikesMemoryCache()
        {
            cache = MemoryCache.Default;
            cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddSeconds(30);
        }


        public void AddToCache(string key, Object o)
        {
            CacheItem ci = new CacheItem(key, o);
            cache.Add(ci,cacheItemPolicy);

        }

        public object CheckCache(string key)
        {
            if (cache.Contains(key))
            {
                return cache.Get(key);
            }
            else return null;
        }
        
    }
}