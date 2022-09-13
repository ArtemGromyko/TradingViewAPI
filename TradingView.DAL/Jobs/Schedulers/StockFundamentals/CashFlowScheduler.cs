using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockFundamentals;

namespace TradingView.DAL.Jobs.Schedulers.StockFundamentals;
public static class CashFlowScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<CashFlowJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("CashFlowTrigger", "default")
            .StartNow()
            .WithCronSchedule("30 0 8,9 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updates at 8am, 9am UTC daily
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
