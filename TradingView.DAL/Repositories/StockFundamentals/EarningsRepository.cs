using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.StockFundamentals;
public class EarningsRepository : RepositoryBase<EarningsEntity>, IEarningsRepository
{
    public EarningsRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
       : base(settings, configuration["MongoDBCollectionNames:EarningsCollectionName"])
    {
    }
}
