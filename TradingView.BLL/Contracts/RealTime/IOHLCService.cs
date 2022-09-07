using TradingView.DAL.Entities.RealTime.OHLC;

namespace TradingView.BLL.Contracts.RealTime;

public interface IOHLCService
{
    Task<OHLC> GetOHLCAsync(string symbol);
}
