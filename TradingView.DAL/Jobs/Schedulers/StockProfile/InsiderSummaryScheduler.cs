using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockProfile;

namespace TradingView.DAL.Jobs.Schedulers.StockProfile;
public static class InsiderSummaryScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<InsiderSummaryJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("InsiderSummaryTrigger", "default")
            .StartNow()
            .WithCronSchedule("30 0 9,10 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updates at 5am, 6am ET every day
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
