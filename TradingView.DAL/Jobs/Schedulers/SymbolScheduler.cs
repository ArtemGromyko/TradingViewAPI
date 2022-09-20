using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs;

namespace TradingView.DAL.Jobs.Schedulers
{
    public static class SymbolScheduler
    {
        public static async void Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<SymbolJob>()
                .WithIdentity("J_Symbol", "J_StockProfile")
                .StoreDurably()
                .Build();

            await scheduler.AddJob(job, true);

            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("SymbolTrigger", "default")
            //    .ForJob(job)
            //    .WithSchedule(CronScheduleBuilder
            //    .DailyAtHourAndMinute(8, 0)
            //    .InTimeZone(TimeZoneInfo.Utc))
            //    .Build();

            ITrigger triggerStart = TriggerBuilder.Create()
                 .WithIdentity("SymbolStart", "default")
                 .ForJob(job)
                 .WithSimpleSchedule(x => x
                     .WithIntervalInSeconds(1)
                     .WithRepeatCount(0))
                 .Build();

            //await scheduler.ScheduleJob(trigger);
            await scheduler.ScheduleJob(triggerStart);
        }
    }
}
