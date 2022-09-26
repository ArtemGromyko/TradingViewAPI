using TradingView.BLL.Models.Response;

namespace TradingView.BLL.Contracts
{
    public interface IStockProfileService
    {
        Task<StockProfileDto> GetAsync(string symbol, CancellationToken ct = default);
    }
}
