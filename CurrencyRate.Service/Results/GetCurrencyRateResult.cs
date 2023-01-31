namespace CurrencyRate.Service.Results
{
    public class GetCurrencyRateResult : BaseResult
    {
        public IEnumerable<decimal> Currences { get; set; } = null!;
    }
}
