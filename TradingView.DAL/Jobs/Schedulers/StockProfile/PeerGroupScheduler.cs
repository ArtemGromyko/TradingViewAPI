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

        IJobDetail jobDetail = JobBuilder.Create<PeerGroupJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("LogoTrigger", "default")
            .StartNow()
            .WithCronSchedule("0 0 8 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //8am UTC daily
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
