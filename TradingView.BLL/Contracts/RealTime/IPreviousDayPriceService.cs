using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Contracts.RealTime;

public interface IPreviousDayPriceService
{
    Task<PreviousDayPrice> GetPreviousDayPriceAsync(string symbol);
}
