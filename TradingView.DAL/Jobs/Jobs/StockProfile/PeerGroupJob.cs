using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.StockProfile;

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
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var repository = scope.ServiceProvider.GetService<IPeerGroupRepository>();
            await repository.DeleteAllAsync();
        }
    }
}