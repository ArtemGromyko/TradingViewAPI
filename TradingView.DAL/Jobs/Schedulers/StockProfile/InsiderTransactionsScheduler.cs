using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockProfile;

namespace TradingView.DAL.Jobs.Schedulers.StockProfile;
public static class InsiderTransactionsScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail job = JobBuilder.Create<InsiderTransactionsJob>()
            .Build();

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("InsiderTransactionsTrigger", "default")
            .WithCronSchedule("0 0 0 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updates at UTC every day
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}
