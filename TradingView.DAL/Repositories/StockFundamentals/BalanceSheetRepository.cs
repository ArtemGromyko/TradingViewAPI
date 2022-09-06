using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.StockFundamentals;
public class BalanceSheetRepository : RepositoryBase<BalanceSheetEntity>, IBalanceSheetRepository
{
    public BalanceSheetRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
       : base(settings, configuration["MongoDBCollectionNames:BalanceSheetCollectionName"])
    {
    }
}
