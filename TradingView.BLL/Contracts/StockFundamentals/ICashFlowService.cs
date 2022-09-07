using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Contracts.StockFundamentals;
public interface ICashFlowService
{
    Task<CashFlowEntity> GetAsync(string symbol, CancellationToken ct = default);
}
