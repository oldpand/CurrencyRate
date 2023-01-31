using CurrencyRate.Service.Results;
using MediatR;

namespace CurrencyRate.Service.Commands
{
    public class GetCurrensyRateQuery : IRequest<GetCurrencyRateResult>
    {
        public GetCurrensyRateQuery(string from, string to, bool isHistory)
        {
            From = from;
            To = to;
            IsHistory = isHistory;
        }

        public string From { get; }
        public string To { get; }
        public bool IsHistory { get; }
    }
}
