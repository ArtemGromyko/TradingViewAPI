using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Contracts.RealTime;

public interface IIntradayPricesService
{
    Task<List<IntradayPrice>> GetIntradayPricesListAsync(string symbol);
}
