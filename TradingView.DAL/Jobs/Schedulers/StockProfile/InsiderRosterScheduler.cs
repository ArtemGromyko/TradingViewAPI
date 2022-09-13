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

        IJobDetail jobDetail = JobBuilder.Create<InsiderRosterJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("InsiderRosterTrigger", "default")
            .StartNow()
            .WithCronSchedule("0 0 4,8 ? * * *"/*, x => x.InTimeZone(TimeZoneInfo.Utc)*/) //Updates at 5am, 6am ET every day
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
