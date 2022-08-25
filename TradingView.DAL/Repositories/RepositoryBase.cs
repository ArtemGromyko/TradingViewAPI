using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TradingView.DAL.Contracts;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly IMongoCollection<TEntity> _collection;

    public RepositoryBase(IOptions<DatabaseSettings> settings, string collectionName)
    {
        var mongoClient = new MongoClient(
            settings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            settings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<TEntity>(collectionName);
    }

    public async Task AddCollectionAsync(IEnumerable<TEntity> collection) =>
        await _collection.InsertManyAsync(collection);

    public async Task<List<TEntity>> GetAllAsync() =>
        await _collection.AsQueryable().ToListAsync();
}
