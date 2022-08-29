using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Contracts.StockProfile;
public interface ILogoService
{
    Task<Logo> GetLogoAsync(string symbol, CancellationToken ct = default);
}
