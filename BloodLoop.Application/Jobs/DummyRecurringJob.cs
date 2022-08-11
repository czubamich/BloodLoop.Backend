using BloodCore;
using BloodCore.Workers;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Jobs
{
    [Injectable]
    public class DummyRecurringJob : IRecurringJob
    {
        public string Name => nameof(DummyRecurringJob);

        private readonly IJobScheduler _scheduler;

        public DummyRecurringJob(IJobScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public Task LogDummyData()
        {
            Log.Information($"{nameof(DummyRecurringJob)} has been executed succesfully");

            Random rnd = new Random();

            _scheduler.Schedule<DummyRecurringJob>(x => x.LogMoreData(rnd.Next(100)), TimeSpan.FromMinutes(3), out _);

            return Task.CompletedTask;
        }

        public Task LogMoreData(int value)
        {
            Log.Information($"{nameof(DummyRecurringJob)} has logged even more data: {{Data}}", value);

            return Task.CompletedTask;
        }

        public void Configure(IJobScheduler scheduler, RecurringJobDefinition settings)
        {
            scheduler.AddOrReplaceRecurring<DummyRecurringJob>(settings.JobName, x => x.LogDummyData(), settings.Cron);
        }
    }
}
