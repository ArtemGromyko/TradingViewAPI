using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime.LargestTrade;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.RealTime;

public class LargestTradesRepository : RepositoryBase<LargestTrade>, ILargestTradesRepository
{
    public LargestTradesRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
        : base(settings, configuration["MongoDBCollectionNames:LargestTradesCollectionName"])
    {
    }
}
