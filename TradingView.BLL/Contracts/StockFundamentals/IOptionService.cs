using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Contracts.StockFundamentals;
public interface IOptionService
{
    Task<OptionEntity> GetOptionAsync(string symbol, CancellationToken ct = default);
    Task<List<Expiration>> GetExpirationAsync(string symbol, CancellationToken ct = default);
}
