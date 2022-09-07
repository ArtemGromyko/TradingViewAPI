using Microsoft.AspNetCore.Mvc;
using TradingView.BLL.Contracts.StockFundamentals;

namespace TradingViewAPI.Controllers;
[Route("api")]
[ApiController]
public class StockFundamentalsController : ControllerBase
{
    private readonly IBalanceSheetService _balanceSheetService;
    private readonly ICashFlowService _cashFlowService;
    private readonly IFinancialsService _financialsService;

    public StockFundamentalsController(IBalanceSheetService balanceSheetService, 
        ICashFlowService cashFlowService,
        IFinancialsService financialsService)
    {
        _balanceSheetService = balanceSheetService ?? throw new ArgumentNullException(nameof(balanceSheetService));
        _cashFlowService = cashFlowService ?? throw new ArgumentNullException(nameof(cashFlowService));
        _financialsService = financialsService ?? throw new ArgumentNullException(nameof(financialsService));
    }

    [HttpGet("{symbol}/balance-sheet")]
    public async Task<IActionResult> GetBalanceSheetAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _balanceSheetService.GetAsync(symbol, ct);
        return Ok(result);
    }

    [HttpGet("{symbol}/cash-flow")]
    public async Task<IActionResult> GetCashFlowAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _cashFlowService.GetAsync(symbol, ct);
        return Ok(result);
    }

    [HttpGet("{symbol}/financials")]
    public async Task<IActionResult> GetFinancialsAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _financialsService.GetAsync(symbol, ct);
        return Ok(result);
    }
}
