using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Caching
{
    public class CacheService(IMemoryCache _memoryCache) : ICacheService
    {
        public Task AddAsync<T>(string cacheKey, T value, TimeSpan exprTimeSpan)
        {
            var cache = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = exprTimeSpan,
            };

            _memoryCache.Set(cacheKey, value, cache);
            return Task.CompletedTask;

        }

        public Task<T?> GetAsync<T>(string cacheKey)
        {
            return Task.FromResult(_memoryCache.TryGetValue(cacheKey, out T value) ? value : default(T));
        }

        public Task RemoveAsync<T>(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
            return Task.CompletedTask;
        }
    }
}
