﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.RealTime;

public class QuotesRepository : RepositoryBase<Quote>, IQuotesRepository
{
    public QuotesRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
        : base(settings, configuration["MongoDBCollectionNames:QuotesCollectionName"])
    {
    }
}
