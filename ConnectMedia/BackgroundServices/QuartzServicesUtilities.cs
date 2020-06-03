using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectMedia.BackgroundServices
{
    public class QuartzServicesUtilities
    {
        public static void StartJob<TJob>(IScheduler scheduler)
        where TJob : IJob
        {

            var jobFullName = typeof(TJob).FullName;
            var jobName = typeof(TJob).Name;

            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobFullName)
                .Build();

            if (jobName == "PlayListActivateJob")
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity($"{jobName}.trigger")
                    .StartNow()
                       .WithSimpleSchedule
                     (s =>
                        s.WithInterval(TimeSpan.FromMinutes(1))
                        .RepeatForever()
                     )
                     .Build();
                scheduler.ScheduleJob(job, trigger);
            }
            if (jobName == "PlayListDeActivateJob")
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity($"{jobName}.trigger")
                    .StartNow()
                       .WithSimpleSchedule
                     (s =>
                        s.WithInterval(TimeSpan.FromMinutes(1))
                        .RepeatForever()
                     )
                     .Build();
                scheduler.ScheduleJob(job, trigger);
            }



        }
    }
}
