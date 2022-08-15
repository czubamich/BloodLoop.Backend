using BloodCore.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Infrastructure.IoC
{
    [Settings(nameof(SmtpEmailSettings))]
    public class SmtpEmailSettings
    {
        public string Url { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
