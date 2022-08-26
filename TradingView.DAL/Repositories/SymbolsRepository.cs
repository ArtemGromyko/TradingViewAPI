using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts;
using TradingView.DAL.Entities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories;

public class SymbolsRepository : RepositoryBase<SymbolInfo>, ISymbolsRepository
{
    public SymbolsRepository(IOptions<DatabaseSettings> settings, IConfiguration configurationq)
        : base(settings, configurationq["MongoDBCollectionNames:SymbolsCollectionName"])
    {
    }
}
