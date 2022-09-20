using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class SplitJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SplitJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("SplitJob " + DateTime.Now);
    }
}
