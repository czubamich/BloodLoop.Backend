using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Emails
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(EmailTemplate template, string email);
    }
}
