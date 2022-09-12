using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Contracts.StockFundamentals;
public interface IDividendService
{
    Task<List<Dividend>> GetAsync(string symbol, string range, CancellationToken ct = default);
}
