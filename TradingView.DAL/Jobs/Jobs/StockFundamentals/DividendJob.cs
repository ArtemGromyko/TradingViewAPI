using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class DividendJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DividendJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("DividendJob " + DateTime.Now);
    }
}
