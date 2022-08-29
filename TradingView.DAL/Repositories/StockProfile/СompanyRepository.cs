using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.StockProfile;
internal class СompanyRepository : RepositoryBase<Сompany>, IСompanyRepository
{
    public СompanyRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
        : base(settings, configuration["MongoDBCollectionNames:CompanyCollectionName"])
    {
    }
}
