using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Contracts.RealTime;

public interface IHistoricalPricesService
{
    Task<List<HistoricalPrice>> GetHistoricalPricesListAsync(string symbol);
}
