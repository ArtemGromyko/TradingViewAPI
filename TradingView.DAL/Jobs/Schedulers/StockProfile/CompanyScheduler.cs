using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockProfile;

namespace TradingView.DAL.Jobs.Schedulers.StockProfile;
public static class CompanyScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail job = JobBuilder.Create<CompanyJob>()
            .WithIdentity("J_Company", "J_StockProfile")
        .StoreDurably()
        .Build();

        await scheduler.AddJob(job, true);

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("CompanyTrigger", "default")
            .ForJob(job)
            .WithCronSchedule("0 0 4,5 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updates at 4am and 5am UTC every day
            .Build();

        ITrigger triggerStart = TriggerBuilder.Create()
             .WithIdentity("CompanyStart", "default")
             .ForJob(job)
             .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(1)
                 .WithRepeatCount(0))
             .Build();

        await scheduler.ScheduleJob(trigger);
        await scheduler.ScheduleJob(triggerStart);
    }
}
