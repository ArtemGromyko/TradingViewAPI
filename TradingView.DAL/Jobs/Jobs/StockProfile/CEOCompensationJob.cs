using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockProfile;
public class CEOCompensationJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CEOCompensationJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("CEOCompensationJob " + DateTime.Now);
    }
}
