using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities.RealTime;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.RealTime;

public class DividendsRepository : RepositoryBase<DividendInfo>, IDividendsRepository
{
    public DividendsRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
        : base(settings, configuration["MongoDBCollectionNames:DividendsCollectionName"])
    {
    }
}
