using CurrencyRate.Service.Results;
using MediatR;

namespace CurrencyRate.Service.Commands
{
    public class GetCurrensyRateQuery : IRequest<GetCurrencyRateResult>
    {
        public GetCurrensyRateQuery(string from, string to, bool historical)
        {
            From = from;
            To = to;
            Historical = historical;
        }

        public string From { get; }
        public string To { get; }
        public bool Historical { get; }
    }
}
