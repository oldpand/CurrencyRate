using CurrencyRate.Model;
using Newtonsoft.Json;

namespace CurrencyRate.Service.Services
{
    public class CurrencyRateFileService : ICurrencyRateFileService
    {
        public async Task<IReadOnlyCollection<CurrencyRateRow>> GetRateRowsAsync()
        {
            string path = @"C:\CurrencyRates\Rates.txt";

            using StreamReader r = new StreamReader(path);

            string json = await r.ReadToEndAsync();
            return JsonConvert.DeserializeObject<List<CurrencyRateRow>>(json);
        }
    }
}
