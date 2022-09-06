using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Contracts.StockFundamentals;
public interface IBalanceSheetService
{
    Task<BalanceSheetEntity> GetAsync(string symbol, CancellationToken ct = default);
}
