using Microsoft.AspNetCore.Mvc;
using TradingView.BLL.Contracts.StockFundamentals;

namespace TradingViewAPI.Controllers;
[Route("api")]
[ApiController]
public class StockFundamentalsController : ControllerBase
{
    private readonly IBalanceSheetService _balanceSheetService;

    public StockFundamentalsController(IBalanceSheetService balanceSheetService)
    {
        _balanceSheetService = balanceSheetService ?? throw new ArgumentNullException(nameof(balanceSheetService));
    }

    [HttpGet("{symbol}/balance-sheet")]
    public async Task<IActionResult> GetLogoAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _balanceSheetService.GetAsync(symbol, ct);
        return Ok(result);
    }
}
