using TradingView.DAL.Entities;

namespace TradingView.DAL.Contracts;

public interface IHistoricalPricesRepository
{
    public Task<List<HistoricalPrice>> GetAllHistoricalPricesAsync();
    public Task AddHistoricalPricesCollection(IEnumerable<HistoricalPrice> collection);
}
