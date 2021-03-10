using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Stocktracer.Api.Models;
using Stocktracer.Api.Services;
using Stocktracer.Api.Utils;

namespace Stocktracer.Api.Controllers
{
    [ApiController, Route("[controller]")]
    public class StockController : ControllerBase
    {
        [HttpGet("{symbol}")]
        async public Task<IActionResult> GetFinanceAsync([FromRoute]string symbol, [FromServices]IYahooFinanceService service,[FromServices] IMemoryCache cache)
        {
            try
            {
                //FIXME: Create a abstraction to put value in memory cache 
                var finance = await cache.GetOrCreateAsync(symbol.ToLowerInvariant(), entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);

                    //FIXME: Validates the symbol
                    return service.GetStockFinanceAsync(symbol);
                });
                
                var result = finance.Chart.Results.First();
                var quote = result.Indicators.Quotes.First();
                var firstTradeDate = result.Meta.FirstTradeDate;
                var regularMarketTime = result.Meta.RegularMarketTime;
                var regularMarketPrice = result.Meta.RegularMarketPrice;

                //FIXME: Add application service 
                var symbolFinance = new SymbolFinance
                {
                    FirstTradeDate = firstTradeDate.ToDatetime(),
                    RegularMarketTime = regularMarketTime.ToDatetime(),
                    RegularMarketPrice = regularMarketPrice
                };

                symbolFinance.Indicators = new ReadOnlyCollection<SymbolIndicatorValue>(
                    result
                    .Timestamps
                    .Select((t, i) => new SymbolIndicatorValue(t, quote.Opens[i], quote.Highs[i], quote.Lows[i], quote.Closes[i]))
                    .ToList());

                return Ok(symbolFinance);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
    }
}