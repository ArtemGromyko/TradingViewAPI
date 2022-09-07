﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly ILargestTradesService _largestTradesService;
        private readonly IOHLCService _ohlcService;
        private readonly IPreviousDayPriceService _previousDayPriceService;
        private readonly IPriceOnlyService _priceOnlyService;
        private readonly IVolumeByVenueService _volumeByVenueService;

        public RealTimeController(IHistoricalPricesService historicalPricesService, IQuotesService quotesService,
            IIntradayPricesService intradayPricesService, ILargestTradesService largestTradesService, IOHLCService ohlcService,
            IPreviousDayPriceService previousDayPriceService, IPriceOnlyService priceOnlyService, IVolumeByVenueService volumeByVenueService)
        {
            _historicalPricesService = historicalPricesService;
            _quotesService = quotesService;
            _intradayPricesService = intradayPricesService;
            _largestTradesService = largestTradesService;
            _ohlcService = ohlcService;
            _previousDayPriceService = previousDayPriceService;
            _priceOnlyService = priceOnlyService;
            _volumeByVenueService = volumeByVenueService;
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

        [HttpGet("{symbol}/largest-trades")]
        public async Task<IActionResult> GetLargestTradesAsync(string symbol)
        {
            var largestTrades = await _largestTradesService.GetLargestTradesListAsync(symbol);

            return Ok(largestTrades);
        }

        [HttpGet("{symbol}/ohlc")]
        public async Task<IActionResult> GetOHLCAsync(string symbol)
        {
            var ohlc = await _ohlcService.GetOHLCAsync(symbol);

            return Ok(ohlc);
        }

        [HttpGet("{symbol}/previous-day-price")]
        public async Task<IActionResult> GetPreviousDayPriceAsync(string symbol)
        {
            var previousDayPrice = await _previousDayPriceService.GetPreviousDayPriceAsync(symbol);

            return Ok(previousDayPrice);
        }

        [HttpGet("{symbol}/price")]
        public async Task<IActionResult> GetPriceOnlyAsync(string symbol)
        {
            var priceOnly = await _priceOnlyService.GetPriceOnlyAsync(symbol);

            return Ok(priceOnly);
        }

        [HttpGet("{symbol}/volume-by-venue")]
        public async Task<IActionResult> GetVolumeByVenueAsync(string symbol)
        {
            var volumesByVenue = await _volumeByVenueService.GetVolumesByVenueAsync(symbol);

            return Ok(volumesByVenue);
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
