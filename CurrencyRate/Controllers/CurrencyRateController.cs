using CurrencyRate.Service.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyRate.PublicApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyRateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetExchange(string from, string to, bool historical, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCurrensyRateQuery(from, to, historical), cancellationToken);

            if (!result.IsSuccess)
            {
                BadRequest(result);
            }
            return Ok(result);
        }
    }
}
