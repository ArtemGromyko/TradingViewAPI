using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.StockProfile;
internal class CEOCompensationRepository : RepositoryBase<CEOCompensation>, ICEOCompensationRepository
{
    public CEOCompensationRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
       : base(settings, configuration["MongoDBCollectionNames:CEOCompensationCollectionName"])
    {
    }
}
