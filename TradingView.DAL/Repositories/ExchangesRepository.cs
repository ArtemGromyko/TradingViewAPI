using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class ExchangesRepository : RepositoryBase<ExchangeInfo>, IExchangesRepository
{
    public ExchangesRepository(IOptions<DatabaseSettings> settings)
         : base(settings, "ExchangesCollection")
    {
    }

    public async Task AddExchangesCollection(IEnumerable<ExchangeInfo> collection) =>
        await AddCollectionAsync(collection);

    public async Task<List<ExchangeInfo>> GetAllExchangesAsync() =>
        await GetAllAsync();
}
