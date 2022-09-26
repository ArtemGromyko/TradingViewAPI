using Microsoft.AspNetCore.Mvc;
using TradingView.BLL.Contracts;

namespace TradingViewAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AgrationСontroller : ControllerBase
    {
        private readonly IStockProfileService _stockProfileService;
        private readonly IStockFundamentalsService _stockFundamentalsService;

        public AgrationСontroller(IStockProfileService stockProfileService, IStockFundamentalsService stockFundamentalsService)
        {
            _stockProfileService = stockProfileService;
            _stockFundamentalsService = stockFundamentalsService;
        }

        [HttpGet("{symbol}/stockProfile")]
        public async Task<IActionResult> GetStockProfileAsync(string symbol, CancellationToken ct = default)
        {
            var stockProfile = await _stockProfileService.GetAsync(symbol, ct);
            return Ok(stockProfile);
        }

        [HttpGet("{symbol}/stockFundamentals")]
        public async Task<IActionResult> GetStockFundamentalsAsync(string symbol, CancellationToken ct = default)
        {
            var stockProfile = await _stockFundamentalsService.GetAsync(symbol, ct);
            return Ok(stockProfile);
        }
    }
}
