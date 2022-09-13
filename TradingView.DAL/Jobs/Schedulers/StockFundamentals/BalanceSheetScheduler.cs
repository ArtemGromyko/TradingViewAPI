﻿using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using TradingView.DAL.Jobs.Jobs.StockProfile;

namespace TradingView.DAL.Jobs.Schedulers.StockFundamentals;
public static class BalanceSheetScheduler
{
    public static async void Start(IServiceProvider serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<InsiderSummaryJob>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("InsiderSummaryTrigger", "default")
            .StartNow()
            .WithCronSchedule("0 0 4,8 ? * * *"/*, x => x.InTimeZone(TimeZoneInfo.Utc)*/) //Updates at 8am, 9am UTC daily
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
