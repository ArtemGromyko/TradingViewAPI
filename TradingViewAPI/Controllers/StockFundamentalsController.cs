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
    private readonly IReportedFinancialsService _reportedFinancialsService;
    private readonly IIncomeStatementService _incomeStatementService;
    private readonly ISplitService _splitService;

    public StockFundamentalsController(IBalanceSheetService balanceSheetService,
        ICashFlowService cashFlowService,
        IFinancialsService financialsService,
        IReportedFinancialsService reportedFinancialsService,
        IIncomeStatementService incomeStatementService,
        ISplitService splitService)
    {
        _balanceSheetService = balanceSheetService ?? throw new ArgumentNullException(nameof(balanceSheetService));
        _cashFlowService = cashFlowService ?? throw new ArgumentNullException(nameof(cashFlowService));
        _financialsService = financialsService ?? throw new ArgumentNullException(nameof(financialsService));
        _reportedFinancialsService = reportedFinancialsService ?? throw new ArgumentNullException(nameof(reportedFinancialsService));
        _incomeStatementService = incomeStatementService ?? throw new ArgumentNullException(nameof(incomeStatementService));
        _splitService = splitService ?? throw new ArgumentNullException(nameof(splitService));
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

    [HttpGet("time-series/reported_financials/{symbol}")]
    public async Task<IActionResult> GetReportedFinancialsAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _reportedFinancialsService.GetAsync(symbol, ct);
        return Ok(result);
    }

    [HttpGet("{symbol}/income")]
    public async Task<IActionResult> GetIncomeStatementAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _incomeStatementService.GetAsync(symbol, ct);
        return Ok(result);
    }

    [HttpGet("{symbol}/splits")]
    public async Task<IActionResult> GetSplitsAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _splitService.GetAsync(symbol, ct);
        return Ok(result);
    }
}
