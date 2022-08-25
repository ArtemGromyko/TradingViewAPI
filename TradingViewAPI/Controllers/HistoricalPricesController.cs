using Microsoft.AspNetCore.Mvc;
using TradingView.BLL.Contracts;
using TradingView.DAL.Entities;

namespace TradingViewAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class HistoricalPricesController : ControllerBase
    {
        private readonly IHistoricalPricesService _historicalPricesService;

        public HistoricalPricesController(IHistoricalPricesService historicalPricesService)
        {
            _historicalPricesService = historicalPricesService;
        }

        [HttpGet("historical-prices/{symbol}")]
        public async Task<IActionResult> GetHistoricalPricesAsync(string symbol)
        {
            using var client = new HttpClient();

            var response = await client
                .GetAsync($"https://sandbox.iexapis.com/stable/stock/{symbol}/chart/max?token=Tpk_aae23baa9af74779993006fb85d15f0f");

            var res = await response.Content.ReadAsAsync<IEnumerable<HistoricalPrice>>();

            return Ok(res);
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

        [HttpGet("dividends/{symbol}")]
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
