using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Contracts.StockFundamentals;
public interface ISplitService
{
    Task<List<SplitEntity>> GetAsync(string symbol, CancellationToken ct = default);
    Task<List<SplitEntity>> GetAsync(string symbol, string range, CancellationToken ct = default);
}
