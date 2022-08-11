using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Workers
{
    public interface IJobScheduler
    {
        void Enqueue(Expression<Action> job, out string jobId);
        void Schedule(Expression<Action> job, TimeSpan delay, out string jobId);
        void Schedule(Expression<Action> job, DateTime executionDate, out string jobId);
        void Enqueue<T>(Expression<Action<T>> job, out string jobId);
        void Schedule<T>(Expression<Action<T>> job, TimeSpan delay, out string jobId);
        void Schedule<T>(Expression<Action<T>> job, DateTime executionDate, out string jobId);

        void AddOrReplaceRecurring<T>(string name, Expression<Action<T>> job, string Cron);
        void RemoveRecurring(string name);
    }
}
