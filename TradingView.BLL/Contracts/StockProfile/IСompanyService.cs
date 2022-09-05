using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Contracts.StockProfile;
public interface IСompanyService
{
    Task<Company> GetAsync(string symbol, CancellationToken ct = default);
}
