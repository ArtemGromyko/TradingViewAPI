using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockProfile;
public class InsiderTransactionsJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public InsiderTransactionsJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("InsiderTransactionsJob " + DateTime.Now);
    }
}
