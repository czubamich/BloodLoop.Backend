using BloodCore.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Infrastructure.IoC
{
    [Settings(nameof(EmailSettings))]
    public class EmailSettings
    {
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
