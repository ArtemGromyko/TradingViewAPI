using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class EarningsJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public EarningsJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("EarningsJob " + DateTime.Now);
    }
}
