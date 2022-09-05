using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Contracts.StockProfile;
public interface IInsiderRosterService
{
    Task<InsiderRoster> GetAsync(string symbol, CancellationToken ct = default);
}
