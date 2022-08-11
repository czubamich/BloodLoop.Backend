using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Workers
{
    public interface IRecurringJob
    {
        string Name { get;}
        void Configure(IJobScheduler scheduler, RecurringJobDefinition settings);
    }
}
