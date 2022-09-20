using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class FinancialsJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FinancialsJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("FinancialsJob " + DateTime.Now);
    }
}
