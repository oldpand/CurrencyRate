using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace CurrencyRate.Service.Services
{
    public class CacheService<TItem> : ICacheService<TItem>
    {
        private IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<TItem> GetOrCreateAsync(object key, Func<IConfigurationSection, Task<TItem>> createItem, double cacheTimeMinutes, IConfigurationSection fileConfig)
        {
            TItem cacheEntry;
            if (!_memoryCache.TryGetValue(key, out cacheEntry))
            {
                cacheEntry = await createItem(fileConfig);

                _memoryCache.Set(key, cacheEntry, TimeSpan.FromMinutes(cacheTimeMinutes));
            }
            return cacheEntry;
        }
    }
}
