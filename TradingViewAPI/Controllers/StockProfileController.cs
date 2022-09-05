using Microsoft.AspNetCore.Mvc;
using TradingView.BLL.Contracts.StockProfile;

namespace TradingViewAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StockProfileController : ControllerBase
{
    private readonly ILogoService _logoService;
    private readonly IСompanyService _companyService;
    private readonly ICEOCompensationService _ceoCompensationUrlService;
    private readonly IInsiderRosterService _insiderRosterService;
    private readonly IInsiderTransactionsService _insiderTransactionsService;
    private readonly IInsiderSummaryService _insiderSummaryService;

    public StockProfileController(ILogoService logoService,
        IСompanyService companyService,
        ICEOCompensationService ceoCompensationUrlService,
        IInsiderRosterService insiderRosterService,
        IInsiderTransactionsService insiderTransactionsService,
        IInsiderSummaryService insiderSummaryService)
    {
        _logoService = logoService ?? throw new ArgumentNullException(nameof(logoService));
        _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        _ceoCompensationUrlService = ceoCompensationUrlService ?? throw new ArgumentNullException(nameof(ceoCompensationUrlService));
        _insiderRosterService = insiderRosterService ?? throw new ArgumentNullException(nameof(insiderRosterService));
        _insiderTransactionsService = insiderTransactionsService ?? throw new ArgumentNullException(nameof(insiderTransactionsService));
        _insiderSummaryService = insiderSummaryService ?? throw new ArgumentNullException(nameof(insiderSummaryService));
    }

    [HttpGet("/api/{symbol}/logo")]
    public async Task<IActionResult> GetLogoAsync(string symbol, CancellationToken ct = default)
    {
        var logo = await _logoService.GetAsync(symbol, ct);
        return Ok(logo);
    }

    [HttpGet("/api/{symbol}/company")]
    public async Task<IActionResult> GetCompanyAsync(string symbol, CancellationToken ct = default)
    {
        var reponse = await _companyService.GetAsync(symbol, ct);
        return Ok(reponse);
    }

    [HttpGet("/api/{symbol}/ceo-compensation")]
    public async Task<IActionResult> GetCEOCompensationAsync(string symbol, CancellationToken ct = default)
    {
        var reponse = await _ceoCompensationUrlService.GetAsync(symbol, ct);
        return Ok(reponse);
    }

    [HttpGet("/api/{symbol}/insider-roster")]
    public async Task<IActionResult> GetInsiderRosterAsync(string symbol, CancellationToken ct = default)
    {
        var reponse = await _insiderRosterService.GetAsync(symbol, ct);
        return Ok(reponse);
    }

    [HttpGet("/api/{symbol}/insider-summary")]
    public async Task<IActionResult> GetInsiderSummaryAsync(string symbol, CancellationToken ct = default)
    {
        var reponse = await _insiderSummaryService.GetAsync(symbol, ct);
        return Ok(reponse);
    }

    [HttpGet("/api/{symbol}/insider-transactions")]
    public async Task<IActionResult> GetInsiderTransactionsAsync(string symbol, CancellationToken ct = default)
    {
        var reponse = await _insiderTransactionsService.GetAsync(symbol, ct);
        return Ok(reponse);
    }

    [HttpGet("/api/{symbol}/peers")]
    public async Task<IActionResult> GetPeerGroupsAsync(string symbol, CancellationToken ct = default)
    {
        // var reponse = await _insiderTransactionsService.GetAsync(symbol, ct);
        return Ok();
    }
}
