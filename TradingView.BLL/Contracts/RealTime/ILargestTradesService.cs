using TradingView.DAL.Entities.RealTime.LargestTrade;

namespace TradingView.BLL.Contracts.RealTime;

public interface ILargestTradesService
{
    Task<List<LargestTradeItem>> GetLargestTradesListAsync(string symbol);
}
