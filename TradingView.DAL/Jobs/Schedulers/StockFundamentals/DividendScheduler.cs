using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockFundamentals;

namespace TradingView.DAL.Jobs.Schedulers.StockFundamentals;
public static class DividendScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<DividendJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("DividendTrigger", "default")
            .StartNow()
            .WithCronSchedule("30 0 9 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updated at 9am UTC every day
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
