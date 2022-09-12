using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TradingView.DAL.Contracts.Jobs.Action;

namespace TradingView.DAL.Jobs.Jobs;
public class CEOCompensationJob : IJob
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public CEOCompensationJob(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var emailsender = scope.ServiceProvider.GetService<ICEOCompensationAction>();

            await emailsender.SendEmailAsync("example@gmail.com", "example", "hello");

        }
    }
}
