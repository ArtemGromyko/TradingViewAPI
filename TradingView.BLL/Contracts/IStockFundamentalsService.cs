using TradingView.BLL.Models.Response;

namespace TradingView.BLL.Contracts
{
    public interface IStockFundamentalsService
    {
        Task<StockFundamentalsDto> GetAsync(string symbol, CancellationToken ct = default);
    }
}
