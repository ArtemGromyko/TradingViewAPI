﻿using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs
{
    public class SymbolJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SymbolJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Symbol");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                //var apiService = scope.ServiceProvider.GetService<IStockFundamentalsApiService>();
                //await apiService.GetCashFlowApiAsync();
            }
        }
    }
}
