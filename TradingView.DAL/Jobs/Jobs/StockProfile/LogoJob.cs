using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockProfile;
public class LogoJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LogoJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("LogoJob " + DateTime.Now);
    }
}
