using Microsoft.Extensions.DependencyInjection;
using Quartz;

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
    }
}