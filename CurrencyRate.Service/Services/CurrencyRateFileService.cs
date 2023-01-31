using CurrencyRate.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CurrencyRate.Service.Services
{
    public class CurrencyRateFileService : ICurrencyRateFileService
    {
        private IConfiguration _configuration;

        private ICacheService<CurrencyRateStructure> _cache;

        private const string CacheKey = "CurrencyRateRows";

        public CurrencyRateFileService(IConfiguration configuration, ICacheService<CurrencyRateStructure> cache)
        {
            _configuration = configuration;
            _cache = cache;
        }

        /// <summary>
        /// Получить катеровки дня
        /// </summary>
        /// <returns>Возвращает остсартированную коллекцию катеровок</returns>
        public async Task<CurrencyRateStructure> GetRateOrderedRowsAsync()
        {
            var fileConfig = _configuration.GetRequiredSection("FileService");

            if (fileConfig.GetValue<bool>("IsHardLoad"))
            {
                return await loadRates(fileConfig);
            }

            return await _cache.GetOrCreateAsync(CacheKey, loadRates, fileConfig.GetValue<double>("CacheTime"), fileConfig);
        }

        private Func<IConfigurationSection, Task<CurrencyRateStructure>> loadRates = async (fileConfig) =>
        {
            using StreamReader r = new StreamReader(fileConfig.GetValue<string>("Path")!);
            var json = await r.ReadToEndAsync();
            var list = JsonConvert
                .DeserializeObject<List<CurrencyRateFile>>(json)
                .Where(r => r.DateTime > DateTime.UtcNow.AddDays(-1))
                .OrderByDescending(r => r.DateTime)
                .ToList();

            var collection = new Dictionary<string, List<decimal>>();

            foreach(var item in list)
            {
                var key = item.From + item.To;
                if (collection.ContainsKey(key))
                {
                    collection[key].Add(item.Currency);
                }
                else
                {
                    collection.Add(key, new List<decimal> { item.Currency });
                }
            }

            return new CurrencyRateStructure { Rates = collection };
        };
    }
}
