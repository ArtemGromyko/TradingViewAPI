﻿using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.ApiServices;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class DividendJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DividendJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var apiService = scope.ServiceProvider.GetService<IStockFundamentalsApiService>();
            //await apiService.GeDividendtApiAsync();
        }
    }
}
