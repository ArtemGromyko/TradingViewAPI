using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockProfile;

namespace TradingView.DAL.Jobs.Schedulers.StockProfile;
public static class CEOCompensationScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail job = JobBuilder.Create<CEOCompensationJob>()
            .Build();

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("CEOCompensationTrigger", "default")
             .WithCronSchedule("0 0 1 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc))//1am daily
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}
