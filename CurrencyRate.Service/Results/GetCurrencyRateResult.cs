namespace CurrencyRate.Service.Results
{
    public class GetCurrencyRateResult : BaseResult
    {
        public IReadOnlyCollection<decimal> Currences { get; set; } = null!;
    }
}
