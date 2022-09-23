using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.RealTime;

namespace TradingView.DAL.Jobs.Jobs.RealTime
{
    public class OHLCJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OHLCJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<IOHLCRepository>();
                await repository.DeleteAllAsync();
            }
        }
    }
}
