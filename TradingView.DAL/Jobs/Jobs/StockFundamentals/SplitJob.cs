using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.StockFundamentals;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class SplitJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SplitJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var repository = scope.ServiceProvider.GetService<ISplitRepository>();
            await repository.DeleteAllAsync();
        }
    }
}
