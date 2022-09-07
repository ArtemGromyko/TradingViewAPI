using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Contracts.StockFundamentals;
public interface IFinancialsService
{
    Task<FinancialsEntity> GetAsync(string symbol, CancellationToken ct = default);
}
