using CurrencyRate.Service.Commands;
using CurrencyRate.Service.Results;
using MediatR;
using CurrencyRate.Service.Services;
using Microsoft.Extensions.Logging;

namespace CurrencyRate.Service.Handlers
{
    public class GetCurrensyRateQueryHandler : IRequestHandler<GetCurrensyRateQuery, GetCurrencyRateResult>
    {
        private ICurrencyRateFileService _currencyRateFileService;

        private ILogger<GetCurrensyRateQueryHandler> _logger;

        public GetCurrensyRateQueryHandler(ICurrencyRateFileService currencyRateFileService, ILogger<GetCurrensyRateQueryHandler> logger)
        {
            _currencyRateFileService = currencyRateFileService;
            _logger = logger;
        }

        public async Task<GetCurrencyRateResult> Handle(GetCurrensyRateQuery request, CancellationToken cancellationToken)
        {
            var result = new GetCurrencyRateResult();

            try
            {
                var rows = await _currencyRateFileService.GetRateRowsAsync();

                _logger.LogInformation("Rows loaded");

                if (request.IsHistory)
                {
                    var resultCollection = rows
                        .Where(r => r.From == request.From &&
                                    r.To == request.To &&
                                    r.DateTime >= DateTime.UtcNow.AddDays(-1))
                        .OrderByDescending(r => r.DateTime)
                        .Select(r => r.Currency)
                        .ToList();

                    result.Currences = resultCollection;
                    return result;
                }

                var resultRow = rows
                    .OrderByDescending(r => r.DateTime)
                    .First(r => r.From == request.From &&
                                r.To == request.To &&
                                r.DateTime >= DateTime.UtcNow.AddDays(-1))
                    .Currency;

                result.Currences = new List<decimal> { resultRow };
                return result;
            }
            catch (FileLoadException ex)
            {
                _logger.LogError("Error with file {Message}", ex.Message);
                result.AddError(new Error(-1, ex.Message));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while reading rows with request {Request}, Error: {Message}", request, ex.Message);
                result.AddError(new Error(-1, ex.Message));
                return result;
            }
        }
    }
}
