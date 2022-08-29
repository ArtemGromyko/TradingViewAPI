﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.StockProfile;

public class InsiderSummaryRepository : RepositoryBase<InsiderSummaryItem>, IInsiderSummaryRepository
{
    public InsiderSummaryRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
      : base(settings, configuration["MongoDBCollectionNames:InsiderSummaryCollectionName"])
    {
    }
}