using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingView.BLL.Contracts;

namespace TradingViewAPI.Controllers
{
    [Route("api/symbols")]
    [ApiController]
    public class SymbolController : ControllerBase
    {
        private readonly ISymbolService _symbolService;

        public SymbolController(ISymbolService symbolService)
        {
            _symbolService = symbolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSymbolsAsync()
        {
            var symbols = await _symbolService.GetSymbolsAsync();

            return Ok(symbols);
        }
    }
}
