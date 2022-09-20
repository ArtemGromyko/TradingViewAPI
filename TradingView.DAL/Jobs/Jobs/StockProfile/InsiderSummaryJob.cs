using Microsoft.Extensions.DependencyInjection;
using Quartz;

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
        Console.WriteLine("InsiderSummaryJob " + DateTime.Now);
    }
}
