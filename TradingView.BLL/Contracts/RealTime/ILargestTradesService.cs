using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Contracts.RealTime;

public interface ILargestTradesService
{
    Task<List<LargestTrade>> GetLargestTradesListAsync(string symbol);
}
