using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class CashFlowJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CashFlowJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("CashFlowJob " + DateTime.Now);
    }
}
