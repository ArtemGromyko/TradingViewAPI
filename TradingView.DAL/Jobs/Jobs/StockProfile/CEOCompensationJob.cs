using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;

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
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var repository = scope.ServiceProvider.GetService<ICEOCompensationRepository>();
            await repository.DeleteAllAsync();
        }
    }
}
