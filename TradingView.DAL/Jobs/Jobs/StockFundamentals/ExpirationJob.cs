using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class ExpirationJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ExpirationJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("ExpirationJob " + DateTime.Now);
    }
}
