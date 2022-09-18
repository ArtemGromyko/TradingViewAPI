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

        IJobDetail jobDetail = JobBuilder.Create<CompanyJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("CompanyTrigger", "default")
            .StartNow()
            .WithCronSchedule("0 0 4,5 ? * * *", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updates at 4am and 5am UTC every day
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
