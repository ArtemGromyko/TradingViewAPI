using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Contracts.StockProfile;
public interface IInsiderTransactionsService
{
    Task<List<InsiderTransactionsItem>> GetAsync(string symbol, CancellationToken ct = default);
}
