using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class ReportedFinancialsJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ReportedFinancialsJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("ReportedFinancialsJob " + DateTime.Now);
    }
}
