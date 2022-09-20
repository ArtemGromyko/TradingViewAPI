using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class OptionJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public OptionJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("OptionJob " + DateTime.Now);
    }
}
