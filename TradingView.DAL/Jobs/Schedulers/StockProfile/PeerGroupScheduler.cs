using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockProfile;

namespace TradingView.DAL.Jobs.Schedulers.StockProfile;
public static class PeerGroupScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail job = JobBuilder.Create<PeerGroupJob>()
            .Build();

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("PeerGroupTrigger", "default")
            .WithSchedule(CronScheduleBuilder
            .DailyAtHourAndMinute(8, 0)
            .InTimeZone(TimeZoneInfo.Utc))
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}
