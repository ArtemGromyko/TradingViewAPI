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

    public StockProfileController(ILogoService logoService,
        IСompanyService companyService,
        ICEOCompensationService ceoCompensationUrlService)
    {
        _logoService = logoService ?? throw new ArgumentNullException(nameof(logoService));
        _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        _ceoCompensationUrlService = ceoCompensationUrlService ?? throw new ArgumentNullException(nameof(ceoCompensationUrlService));
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
        var company = await _companyService.GetAsync(symbol, ct);
        return Ok(company);
    }

    [HttpGet("/stock/{symbol}/ceo-compensation")]
    public async Task<IActionResult> GetCEOCompensationAsync(string symbol, CancellationToken ct = default)
    {
        var company = await _ceoCompensationUrlService.GetAsync(symbol, ct);
        return Ok(company);
    }
}
