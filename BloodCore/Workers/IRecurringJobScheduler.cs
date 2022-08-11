using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Workers
{
    public interface IRecurringJobScheduler
    {
        public Task Configure(IJobScheduler jobScheduler);
    }
}
