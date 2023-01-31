using Microsoft.Extensions.Configuration;

namespace CurrencyRate.Service.Services
{
    public interface ICacheService<TItem>
    {
        public Task<TItem> GetOrCreateAsync(object key, Func<IConfigurationSection, Task<TItem>> createItem, double cacheTimeMinutes, IConfigurationSection fileConfig);
    }
}
