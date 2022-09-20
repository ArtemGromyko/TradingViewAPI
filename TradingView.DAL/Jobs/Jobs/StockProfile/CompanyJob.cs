using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockProfile;
public class CompanyJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CompanyJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("CompanyJob " + DateTime.Now);
    }
}
