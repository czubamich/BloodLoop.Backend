using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Workers
{
    public interface IJobScheduler
    {
        void Enqueue(Func<ValueTask> job);
        void Enqueue(Func<ValueTask> job, TimeSpan delay);
        void Enqueue(Func<ValueTask> job, DateTime executionDate);
        void Enqueue<T>(Func<T, ValueTask> job);
        void Enqueue<T>(Func<T, ValueTask> job, TimeSpan delay);
        void Enqueue<T>(Func<T, ValueTask> job, DateTime executionDate);

        void ConfigureRecurringJobs();
    }
}
