using CurrencyRate.Model;

namespace CurrencyRate.Service.Services
{
    public interface ICurrencyRateFileService
    {
        Task<IReadOnlyCollection<CurrencyRateRow>> GetRateRowsAsync();
    }
}
