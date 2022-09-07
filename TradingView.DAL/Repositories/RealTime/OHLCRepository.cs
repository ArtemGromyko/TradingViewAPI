using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime.OHLC;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.RealTime;

public class OHLCRepository : RepositoryBase<OHLC>, IOHLCRepository
{
    public OHLCRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
        : base(settings, configuration["MongoDBCollectionNames:OHLCCollectionName"])
    {
    }
}
