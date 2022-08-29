using Microsoft.AspNetCore.Mvc;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingViewAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StockProfileController : ControllerBase
{
    private readonly ILogoService _logoService;
    private readonly IСompanyService _companyService;

    public StockProfileController(ILogoService logoService, IСompanyService companyService)
    {
        _logoService = logoService ?? throw new ArgumentNullException(nameof(logoService));
        _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
    }

    [HttpGet("/stock/{symbol}/logo")]
    public async Task<IActionResult> GetLogoAsync(string symbol, CancellationToken ct = default)
    {
       var logo = await _logoService.GetLogoAsync(symbol, ct);
        return Ok(logo);
    }

    [HttpGet("/stock/{symbol}/company")]
    public async Task<IActionResult> GetCompanyAsync(string symbol, CancellationToken ct = default)
    {
        var company = await _companyService.GetCompanyAsync(symbol, ct);
        return Ok(company);
    }
}
