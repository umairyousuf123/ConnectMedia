using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.Scheduler
{
    public class JobScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            IJobDetail jobNotice = JobBuilder.Create<DeactivateNotice>().Build();
            IJobDetail jobClassified = JobBuilder.Create<DeactivateNotice>().Build();
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("NoticeUpdate", "Group1")
            .StartNow()
            .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(10)
            .RepeatForever())
            .Build();
            await scheduler.ScheduleJob(jobNotice, trigger);
            await scheduler.ScheduleJob(jobClassified, trigger);
        }
    }
}
