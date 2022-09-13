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

        IJobDetail jobDetail = JobBuilder.Create<CEOCompensationJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("CEOCompensationTrigger", "default")
            .StartNow()
            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(13, 1)) //1am daily
            /*x => x
            .WithIntervalInSeconds(5)
            .RepeatForever()*/
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
