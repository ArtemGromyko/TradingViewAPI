using TradingView.DAL.Entities.RealTime;

namespace TradingView.BLL.Contracts.RealTime;

public interface IRealTimeService
{
    Task<List<DividendInfo>> GetAllDividendsAsync();
    Task<List<HistoricalPrice>> GetAllHistoricalPricesAsync(string symbol);
    Task<List<ExchangeInfo>> GetAllExchanges();
}
