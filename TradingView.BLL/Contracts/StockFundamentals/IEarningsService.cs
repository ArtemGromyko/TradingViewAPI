using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Contracts.StockFundamentals;
public interface IEarningsService
{
    Task<EarningsEntity> GetAsync(string symbol, CancellationToken ct = default);
    Task<EarningsEntity> GetAsync(string symbol, int last, CancellationToken ct = default);
}
