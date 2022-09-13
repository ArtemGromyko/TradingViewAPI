using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.ApiServices;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class OptionJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public OptionJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var apiService = scope.ServiceProvider.GetService<IStockFundamentalsApiService>();
            await apiService.GetOptionApiAsync();
        }
    }
}
