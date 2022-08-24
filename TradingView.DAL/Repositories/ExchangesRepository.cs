using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class ExchangesRepository : IExchangesRepository
{
    private readonly IMongoCollection<ExchangeInfo> _exchangesCollection;

    public ExchangesRepository(IOptions<ExchangesCollectionSettings> exchangesCollectionSettings)
    {
        var mongoClient = new MongoClient(
            exchangesCollectionSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            exchangesCollectionSettings.Value.DatabaseName);

        _exchangesCollection = mongoDatabase.GetCollection<ExchangeInfo>(
            exchangesCollectionSettings.Value.ExchangesCollectionName);
    }

    public async Task AddExchangesCollection(IEnumerable<ExchangeInfo> collection) =>
        await _exchangesCollection.InsertManyAsync(collection);

    public async Task<List<ExchangeInfo>> GetAllExchangesAsync() =>
        await _exchangesCollection.AsQueryable().ToListAsync();
}
