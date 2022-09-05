using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Services.StockProfile;
public class LogoService : ILogoService 
{
    private readonly ILogoRepository _logoRepository;

    public LogoService(ILogoRepository logoRepository)
    {
        _logoRepository = logoRepository ?? throw new ArgumentNullException(nameof(logoRepository));
    }

    public async Task<Logo> GetLogoAsync(string symbol, CancellationToken ct = default)
    {
        return await _logoRepository.GetAsync(x => x.Symbol == symbol, ct);
    }
}
