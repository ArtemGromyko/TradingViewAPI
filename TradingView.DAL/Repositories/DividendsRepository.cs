using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class DividendsRepository : RepositoryBase<DividendInfo>, IDividendsRepository
{
    public DividendsRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration) 
        : base(settings, configuration["MongoDBCollectionNames:DividendsCollectionName"])
    {
    }

    public async Task AddDividendsCollection(IEnumerable<DividendInfo> collection) =>
        await AddCollectionAsync(collection);

    public async Task<List<DividendInfo>> GetAllDividendsAsync() =>
        await GetAllAsync();
}
