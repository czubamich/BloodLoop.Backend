using BloodCore.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Workers
{
    [Settings(nameof(RecurringJobsSettings))]
    public class RecurringJobsSettings
    {
        public List<RecurringJobDefinition> Definitions { get; set; }
    }

    public class RecurringJobDefinition
    {
        public string JobName { get; set; }
        public string Cron { get; set; }
    }
}
