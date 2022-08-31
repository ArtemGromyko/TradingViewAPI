using Microsoft.AspNetCore.Mvc;
using TradingView.BLL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;

namespace TradingViewAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class RealTimeController : ControllerBase
    {
        private readonly IHistoricalPricesService _historicalPricesService;
        private readonly IQuotesService _quotesService;
        private readonly IIntradayPricesService _intradayPricesService;

        public RealTimeController(IHistoricalPricesService historicalPricesService, IQuotesService quotesService, IIntradayPricesService intradayPricesService)
        {
            _historicalPricesService = historicalPricesService;
            _quotesService = quotesService;
            _intradayPricesService = intradayPricesService;
        }

        [HttpGet("{symbol}/historical-prices")]
        public async Task<IActionResult> GetHistoricalPricesListAsync(string symbol)
        {
            var historicalPrices = await _historicalPricesService.GetHistoricalPricesListAsync(symbol);

            return Ok(historicalPrices);
        }

        [HttpGet("{symbol}/quote")]
        public async Task<IActionResult> GetQuoteync(string symbol)
        {
            var quote = await _quotesService.GetQuoteAsync(symbol);

            return Ok(quote);
        }

        [HttpGet("{symbol}/intraday-prices")]
        public async Task<IActionResult> GetIntradayPricesAsync(string symbol)
        {
            var intradayPrices = await _intradayPricesService.GetIntradayPricesListAsync(symbol);

            return Ok(intradayPrices);
        }

        [HttpGet("symbols")]
        public async Task<IActionResult> GetAllSymbolsAsync()
        {
            using var client = new HttpClient();

            var response = await client
                .GetAsync("https://sandbox.iexapis.com/stable/ref-data/symbols?token=Tpk_aae23baa9af74779993006fb85d15f0f");

            var res = await response.Content.ReadAsAsync<IEnumerable<SymbolInfo>>();

            return Ok(res);
        }

        [HttpGet("{symbol}/dividends")]
        public async Task<IActionResult> GetDividendsAsync(string symbol)
        {
            using var client = new HttpClient();

            var response = await client
                .GetAsync($"https://sandbox.iexapis.com/stable/time-series/advanced_dividends/{symbol}?token=Tpk_aae23baa9af74779993006fb85d15f0f");

            var res = await response.Content.ReadAsAsync<IEnumerable<DividendInfo>>();

            return Ok(res);
        }

        [HttpGet("exchanges")]
        public async Task<IActionResult> GetAllExchanges()
        {
            using var client = new HttpClient();

            var response = await client
                .GetAsync($"https://sandbox.iexapis.com/stable/ref-data/exchanges?token=Tpk_aae23baa9af74779993006fb85d15f0f");

            var res = await response.Content.ReadAsAsync<IEnumerable<ExchangeInfo>>();

            return Ok(res);
        }
    }
}
