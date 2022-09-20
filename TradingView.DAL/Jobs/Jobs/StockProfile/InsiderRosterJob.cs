using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockProfile;
public class InsiderRosterJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public InsiderRosterJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("InsiderRosterJob " + DateTime.Now);
    }
}
