using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Contracts.StockProfile;
public interface IInsiderSummaryService
{
    Task<List<InsiderSummaryItem>> GetAsync(string symbol, CancellationToken ct = default);
}
