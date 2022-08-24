using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class DividendsRepository : IDividendsRepository
{
    private readonly IMongoCollection<DividendInfo> _dividendsCollection;

    public DividendsRepository(IOptions<DividendsCollectionSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _dividendsCollection = mongoDatabase.GetCollection<DividendInfo>(
            bookStoreDatabaseSettings.Value.DividendsCollectionName);
    }

    public async Task AddDividendsCollection(IEnumerable<DividendInfo> collection) =>
        await _dividendsCollection.InsertManyAsync(collection);

    public async Task<List<DividendInfo>> GetAllDividendsAsync() =>
        await _dividendsCollection.AsQueryable().ToListAsync();
}
