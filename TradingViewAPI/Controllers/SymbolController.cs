using Microsoft.AspNetCore.Mvc;
using TradingView.BLL.Contracts;

namespace TradingViewAPI.Controllers
{
    [Route("api/symbols")]
    [ApiController]
    public class SymbolController : ControllerBase
    {
        private readonly ISymbolService _symbolService;
        private readonly IExchangeService _exchangeService;

        public SymbolController(ISymbolService symbolService, IExchangeService exchangeService)
        {
            _symbolService = symbolService;
            _exchangeService = exchangeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSymbolsAsync()
        {
            var symbols = await _symbolService.GetSymbolsAsync();

            return Ok(symbols);
        }

        [HttpGet("strings")]
        public async Task<IActionResult> GetSymbolStringsAsync()
        {
            var symbols = await _symbolService.GetSymbolsAsync();

            return Ok(symbols.Select(s => s.Symbol));
        }

        [HttpGet("exchanges")]
        public async Task<IActionResult> GetExchangesAsync()
        {
            var exchanges = await _exchangeService.GetExchangesAsync();

            return Ok(exchanges);
        }
    }
}
