using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class HistoricalPricesRepository : IHistoricalPricesRepository
{
    private readonly IMongoCollection<HistoricalPrice> _historicalPricesCollection;

    public HistoricalPricesRepository(
    IOptions<HistoricalPricesCollectionSettings> historicalPricesCollectionSettings)
    {
        var mongoClient = new MongoClient(
            historicalPricesCollectionSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            historicalPricesCollectionSettings.Value.DatabaseName);

        _historicalPricesCollection = mongoDatabase.GetCollection<HistoricalPrice>(
            historicalPricesCollectionSettings.Value.HistoricalPricesCollectionName);
    }

    public async Task AddHistoricalPricesCollection(IEnumerable<HistoricalPrice> collection) =>
        await _historicalPricesCollection.InsertManyAsync(collection);

    public async Task<List<HistoricalPrice>> GetAllHistoricalPricesAsync() =>
        await _historicalPricesCollection.AsQueryable().ToListAsync();
}
