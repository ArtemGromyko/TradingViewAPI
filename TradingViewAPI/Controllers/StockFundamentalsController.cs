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
    private readonly IOptionService _optionService;
    private readonly IEarningsService _earningsService;

    public StockFundamentalsController(IBalanceSheetService balanceSheetService,
        ICashFlowService cashFlowService,
        IFinancialsService financialsService,
        IReportedFinancialsService reportedFinancialsService,
        IIncomeStatementService incomeStatementService,
        ISplitService splitService,
        IOptionService optionService,
        IEarningsService earningsService)
    {
        _balanceSheetService = balanceSheetService ?? throw new ArgumentNullException(nameof(balanceSheetService));
        _cashFlowService = cashFlowService ?? throw new ArgumentNullException(nameof(cashFlowService));
        _financialsService = financialsService ?? throw new ArgumentNullException(nameof(financialsService));
        _reportedFinancialsService = reportedFinancialsService ?? throw new ArgumentNullException(nameof(reportedFinancialsService));
        _incomeStatementService = incomeStatementService ?? throw new ArgumentNullException(nameof(incomeStatementService));
        _splitService = splitService ?? throw new ArgumentNullException(nameof(splitService));
        _optionService = optionService ?? throw new ArgumentNullException(nameof(optionService));
        _earningsService = earningsService ?? throw new ArgumentNullException(nameof(earningsService));
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

    [HttpGet("{symbol}/splits/{range}/end")]
    public async Task<IActionResult> GetSplitsAsync(string symbol, string range, CancellationToken ct = default)
    {
        // var result = await _splitService.GetAsync(symbol,  ct);
        return Ok();//result);
    }

    [HttpGet("{symbol}/options")]
    public async Task<IActionResult> GetOptionsAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _optionService.GetOptionAsync(symbol, ct);
        return Ok(result);
    }

    [HttpGet("{symbol}/options/{expiration}")]
    public async Task<IActionResult> GetExpirationAsync(string symbol, string expiration, CancellationToken ct = default)
    {
        var result = await _optionService.GetExpirationAsync(symbol, expiration, ct);
        return Ok(result);
    }

    [HttpGet("{symbol}/options/{expiration}/{optionSide}")]
    public async Task<IActionResult> GetExpirationAsync(string symbol, string expiration, string optionSide, CancellationToken ct = default)
    {
        var result = await _optionService.GetExpirationAsync(symbol, expiration, optionSide, ct);
        return Ok(result);
    }

    [HttpGet("{symbol}/earnings/{last}/end")]
    public async Task<IActionResult> GetEarningsAsync(string symbol, int last, CancellationToken ct = default)
    {
        var result = await _earningsService.GetAsync(symbol, last, ct);
        return Ok(result);
    }

    [HttpGet("{symbol}/earnings")]
    public async Task<IActionResult> GetEarningsAsync(string symbol, CancellationToken ct = default)
    {
        var result = await _earningsService.GetAsync(symbol, ct);
        return Ok(result);
    }
}
