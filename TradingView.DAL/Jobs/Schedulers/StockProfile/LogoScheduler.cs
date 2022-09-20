using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockProfile;

namespace TradingView.DAL.Jobs.Schedulers.StockProfile;
public static class LogoScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail job = JobBuilder.Create<LogoJob>()
            .WithIdentity("J_Logo", "J_StockProfile")
            .StoreDurably()
            .Build();

        await scheduler.AddJob(job, true);

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("LogoTrigger", "default")
            .ForJob(job)
            .WithCronSchedule("0 0 8 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //8am UTC daily
            .Build();

        ITrigger triggerStart = TriggerBuilder.Create()
             .WithIdentity("LogoTriggerStart", "default")
             .ForJob(job)
             .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(1)
                 .WithRepeatCount(0))
             .Build();

        await scheduler.ScheduleJob(trigger);
        await scheduler.ScheduleJob(triggerStart);
    }
}
