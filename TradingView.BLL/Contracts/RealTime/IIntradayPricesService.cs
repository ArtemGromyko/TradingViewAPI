using TradingView.DAL.Entities.RealTime.IntradayPrice;

namespace TradingView.BLL.Contracts.RealTime;

public interface IIntradayPricesService
{
    Task<List<IntradayPriceItem>> GetIntradayPricesListAsync(string symbol);
}
