using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.ApiServices;

namespace TradingView.DAL.Jobs.Jobs.StockProfile;
public class CEOCompensationJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CEOCompensationJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("CEOCompensationJob " + DateTime.Now);
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var apiService = scope.ServiceProvider.GetService<IStockProfileApiService>();
            await apiService.GetCEOCompensationApiAsync();
        }
        Console.WriteLine("End CEOCompensationJob " + DateTime.Now);
    }
}
