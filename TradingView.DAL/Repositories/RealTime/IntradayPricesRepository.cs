using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime.IntradayPrice;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.RealTime;

public class IntradayPricesRepository : RepositoryBase<IntradayPrice>, IIntradayPricesRepository
{
    public IntradayPricesRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
        : base(settings, configuration["MongoDBCollectionNames:IntradayPricesCollectionName"])
    {
    }
}
