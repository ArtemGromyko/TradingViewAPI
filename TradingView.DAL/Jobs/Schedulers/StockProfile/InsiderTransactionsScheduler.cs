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
            .WithIdentity("J_InsiderTransactions", "J_StockProfile")
            .StoreDurably()
            .Build();

        await scheduler.AddJob(job, true);

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("InsiderTransactionsTrigger", "default")
            .ForJob(job)
            .WithCronSchedule("30 0 9,10 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updates at UTC every day
            .Build();

        ITrigger triggerStart = TriggerBuilder.Create()
             .WithIdentity("InsiderTransactionsTriggerStart", "default")
             .ForJob(job)
             .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(1)
                 .WithRepeatCount(0))
             .Build();

        await scheduler.ScheduleJob(trigger);
        await scheduler.ScheduleJob(triggerStart);
    }
}
