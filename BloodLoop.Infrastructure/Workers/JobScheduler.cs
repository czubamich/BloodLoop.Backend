using BloodCore;
using BloodCore.Emails;
using BloodCore.Workers;
using BloodLoop.Infrastructure.Settings;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Infrastructure.Workers
{
    [Injectable]
    internal class JobScheduler : IJobScheduler
    {
        public void Enqueue(Expression<Action> job, out string jobId)
        {
            jobId = BackgroundJob.Enqueue(job);
        }

        public void Schedule(Expression<Action> job, TimeSpan delay, out string jobId)
        {
            jobId = BackgroundJob.Schedule(job, delay);
        }

        public void Schedule(Expression<Action> job, DateTime executionDate, out string jobId)
        {
            jobId = BackgroundJob.Schedule(job, executionDate);
        }

        public void Enqueue<T>(Expression<Action<T>> job, out string jobId)
        {
            jobId = BackgroundJob.Enqueue(job);
        }

        public void Schedule<T>(Expression<Action<T>> job, TimeSpan delay, out string jobId)
        {
            jobId = BackgroundJob.Schedule(job, delay);
        }

        public void Schedule<T>(Expression<Action<T>> job, DateTime executionDate, out string jobId)
        {
            jobId = BackgroundJob.Schedule(job, executionDate);
        }

        public void AddOrReplaceRecurring<T>(string name, Expression<Action<T>> job, string cron)
        {
            RecurringJob.AddOrUpdate(name, job, cron);
        }

        public void RemoveRecurring(string name)
        {
            RecurringJob.RemoveIfExists(name);
        }
    }
}
