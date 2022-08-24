using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class SymbolsRepository : ISymbolsRepository
{
    private readonly IMongoCollection<SymbolInfo> _symbolsCollection;

    public SymbolsRepository(
    IOptions<SymbolsCollectionSettings> symbolsCollectionSettings)
    {
        var mongoClient = new MongoClient(
            symbolsCollectionSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            symbolsCollectionSettings.Value.DatabaseName);

        _symbolsCollection = mongoDatabase.GetCollection<SymbolInfo>(
            symbolsCollectionSettings.Value.SymbolsCollectionName);
    }

    public async Task AddSymbolsCollection(IEnumerable<SymbolInfo> collection) =>
        await _symbolsCollection.InsertManyAsync(collection);

    public async Task<List<SymbolInfo>> GetAllSymbolsAsync() =>
        await _symbolsCollection.AsQueryable().ToListAsync();
}
