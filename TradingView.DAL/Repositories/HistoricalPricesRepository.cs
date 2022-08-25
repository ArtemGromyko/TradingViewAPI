using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class HistoricalPricesRepository : RepositoryBase<HistoricalPrice>, IHistoricalPricesRepository
{
    public HistoricalPricesRepository(IOptions<DatabaseSettings> settings)
         : base(settings, "HistoricalPricesCollection")
    {
    }

    public async Task AddHistoricalPricesCollection(IEnumerable<HistoricalPrice> collection) =>
        await AddCollectionAsync(collection);

    public async Task<List<HistoricalPrice>> GetAllHistoricalPricesAsync() =>
        await GetAllAsync();
}
