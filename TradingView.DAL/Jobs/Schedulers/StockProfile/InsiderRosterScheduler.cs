using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockProfile;

namespace TradingView.DAL.Jobs.Schedulers.StockProfile;
public static class InsiderRosterScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail job = JobBuilder.Create<InsiderRosterJob>()
            .WithIdentity("J_InsiderRoster", "J_StockProfile")
            .StoreDurably()
            .Build();

        await scheduler.AddJob(job, true);

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("InsiderRosterTrigger", "default")
            .ForJob(job)
            .WithCronSchedule("30 0 9,10 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updates at 5am, 6am ET every day
            .Build();

        ITrigger triggerStart = TriggerBuilder.Create()
             .WithIdentity("InsiderRosterStart", "default")
             .ForJob(job)
             .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(1)
                 .WithRepeatCount(0))
             .Build();

        await scheduler.ScheduleJob(trigger);
        await scheduler.ScheduleJob(triggerStart);
    }
}
