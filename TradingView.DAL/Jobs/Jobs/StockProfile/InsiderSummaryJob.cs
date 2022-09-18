using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.ApiServices;

namespace TradingView.DAL.Jobs.Jobs.StockProfile;
public class InsiderSummaryJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public InsiderSummaryJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var apiService = scope.ServiceProvider.GetService<IStockProfileApiService>();
            await apiService.GetInsiderSummaryApiAsync();
        }
    }
}
