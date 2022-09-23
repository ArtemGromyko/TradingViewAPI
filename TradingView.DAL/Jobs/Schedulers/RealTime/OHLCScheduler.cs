using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.RealTime;

namespace TradingView.DAL.Jobs.Schedulers.RealTime
{
    public static class OHLCScheduler
    {
        public static async void Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<OHLCJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("CashFlowTrigger", "default")
                .StartNow()
                .WithCronSchedule("0 30 13 ? * MON,TUE,WED,THU,FRI *\"", x => x.InTimeZone(TimeZoneInfo.Utc)) //Updates at 8am, 9am UTC daily
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
