using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Contracts.StockProfile;
public interface ICEOCompensationService
{
    Task<CEOCompensation> GetAsync(string symbol, CancellationToken ct = default);
}
