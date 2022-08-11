﻿using BloodCore.Extensions;
using BloodCore.Workers;
using BloodLoop.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodLoop.Infrastructure.Workers
{
    public static class JobSchedulerServiceExtensions
    {
        public static IServiceProvider SetupRecurringJobs(this IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var reccuringJobsSettingsSection = configuration.GetSection(nameof(RecurringJobsSettings));
            var reccuringJobsSettings = reccuringJobsSettingsSection.Get<RecurringJobsSettings>();

            if (reccuringJobsSettings?.Definitions is null || reccuringJobsSettings.Definitions.Count == 0)
                return serviceProvider;

            var jobScheduler = serviceProvider.GetService<IJobScheduler>();
            var recurringJobs = serviceProvider.GetServices<IRecurringJob>();

            recurringJobs
                .Join(reccuringJobsSettings.Definitions, x => x.Name, x => x.JobName, (rj, s) => (Job: rj, Settings: s))
                .Where(x => x.Job is not null)
                .ForEach(x =>
                {
                    try 
                    {
                        if (!string.IsNullOrEmpty(x.Settings?.Cron))
                            x.Job.Configure(jobScheduler, x.Settings);
                        else
                            jobScheduler.RemoveRecurring(x.Job.Name);
                    }
                    catch(Exception ex)
                    {
                        jobScheduler.RemoveRecurring(x.Job.Name);
                        
                        LogContext.PushProperty("Trace", ex.StackTrace);
                        if(ex.InnerException != null)
                            LogContext.PushProperty("InnerExceptionMessage", ex.InnerException.Message);

                        Log.Error("RecurringJob configuration failed, error: {ExceptionMessage}", ex.Message);
                    }
                });

            return serviceProvider;
        }
    }
}
