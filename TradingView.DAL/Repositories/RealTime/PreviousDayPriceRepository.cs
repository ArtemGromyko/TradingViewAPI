﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TradingView.DAL.Contracts.RealTime;
using TradingView.DAL.Entities.RealTime;
using TradingView.DAL.Settings;

namespace TradingView.DAL.Repositories.RealTime;

public class PreviousDayPriceRepository : RepositoryBase<PreviousDayPrice>, IPreviousDayPriceRepository
{
    public PreviousDayPriceRepository(IOptions<DatabaseSettings> settings, IConfiguration configuration)
        : base(settings, configuration["MongoDBCollectionNames:PreviousDayPriceCollectionName"])
    {
    }
}
