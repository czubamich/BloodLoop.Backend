using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Infrastructure.Settings.WorkScheduler
{
    abstract class JobDefinition
    {
        string Cron { get; set; }
        bool Enabled { get; set; }
    }
}
