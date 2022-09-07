using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.StockFundamentals;
public class CashFlowRepository : RepositoryBase<CashFlowEntity>, ICashFlowRepository
{
    public CashFlowRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
       : base(settings, configuration["MongoDBCollectionNames:CashFlowCollectionName"])
    {
    }
}