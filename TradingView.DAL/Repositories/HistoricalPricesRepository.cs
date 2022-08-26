using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class HistoricalPricesRepository : RepositoryBase<HistoricalPrice>, IHistoricalPricesRepository
{
    public HistoricalPricesRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
        : base(settings, configuration["MongoDBCollectionNames:HistoricalPricesCollectionName"])
    {
    }
}
