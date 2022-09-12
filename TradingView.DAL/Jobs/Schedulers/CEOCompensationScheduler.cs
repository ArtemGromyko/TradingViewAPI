using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs;

namespace TradingView.DAL.Jobs.Schedulers;
public static class CEOCompensationScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<CEOCompensationJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("MailingTrigger", "default")
            //.StartNow()
            .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(5)
            .RepeatForever())
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
