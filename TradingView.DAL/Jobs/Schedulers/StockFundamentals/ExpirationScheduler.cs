using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockFundamentals;

namespace TradingView.DAL.Jobs.Schedulers.StockFundamentals;
public static class ExpirationScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<ExpirationJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("InsiderSummaryTrigger", "default")
            .StartNow()
            .WithCronSchedule("0 0 4,8 ? * * *"/*, x => x.InTimeZone(TimeZoneInfo.Utc)*/) //9:30am ET Mon-Fri
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
