using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.ApiServices;

namespace TradingView.DAL.Jobs.Jobs.StockProfile;
public class PeerGroupJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PeerGroupJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("PeerGroupJob " + DateTime.Now);
        using (var scope = _serviceScopeFactory.CreateScope())
        {

            var apiService = scope.ServiceProvider.GetService<IStockProfileApiService>();
            // await apiService.GetPeerGroupApiAsync();
        }
    }
}
