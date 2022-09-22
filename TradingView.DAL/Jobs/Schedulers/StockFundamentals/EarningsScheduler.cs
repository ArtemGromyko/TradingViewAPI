using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockFundamentals;

namespace TradingView.DAL.Jobs.Schedulers.StockFundamentals;
public static class EarningsScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<EarningsJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("EarningsTrigger", "default")
            .StartNow()
            .WithCronSchedule("0 0 9,11,12 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updates at 9am, 11am, 12pm UTC every day
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
