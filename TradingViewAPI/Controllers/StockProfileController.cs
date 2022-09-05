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

    public StockProfileController(ILogoService logoService,
        IСompanyService companyService,
        ICEOCompensationService ceoCompensationUrlService,
        IInsiderRosterService insiderRosterService)
    {
        _logoService = logoService ?? throw new ArgumentNullException(nameof(logoService));
        _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        _ceoCompensationUrlService = ceoCompensationUrlService ?? throw new ArgumentNullException(nameof(ceoCompensationUrlService));
        _insiderRosterService = insiderRosterService ?? throw new ArgumentNullException(nameof(insiderRosterService));
    }

    [HttpGet("/stock/{symbol}/logo")]
    public async Task<IActionResult> GetLogoAsync(string symbol, CancellationToken ct = default)
    {
        var logo = await _logoService.GetAsync(symbol, ct);
        return Ok(logo);
    }

    [HttpGet("/stock/{symbol}/company")]
    public async Task<IActionResult> GetCompanyAsync(string symbol, CancellationToken ct = default)
    {
        var reponse = await _companyService.GetAsync(symbol, ct);
        return Ok(reponse);
    }

    [HttpGet("/stock/{symbol}/ceo-compensation")]
    public async Task<IActionResult> GetCEOCompensationAsync(string symbol, CancellationToken ct = default)
    {
        var reponse = await _ceoCompensationUrlService.GetAsync(symbol, ct);
        return Ok(reponse);
    }

    [HttpGet("/stock/{symbol}/insider-roster")]
    public async Task<IActionResult> GetInsiderRosterAsync(string symbol, CancellationToken ct = default)
    {
        var reponse = await _insiderRosterService.GetAsync(symbol, ct);
        return Ok(reponse);
    }
}
