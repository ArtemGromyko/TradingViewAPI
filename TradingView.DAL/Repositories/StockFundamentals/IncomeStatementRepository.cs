using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.StockFundamentals;
using TradingView.DAL.Entities.StockFundamentals;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.StockFundamentals;
public class IncomeStatementRepository : RepositoryBase<IncomeStatement>, IIncomeStatementRepository
{
    public IncomeStatementRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
     : base(settings, configuration["MongoDBCollectionNames:IncomeStatementCollectionName"])
    {
    }
}
