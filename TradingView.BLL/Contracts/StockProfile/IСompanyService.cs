using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Contracts.StockProfile;
public interface IСompanyService
{
    Task<Сompany> GetCompanyAsync(string symbol, CancellationToken ct = default);
}
