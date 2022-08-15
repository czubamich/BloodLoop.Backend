using BloodCore.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Emails
{
    public class ConfirmEmailTemplate : BaseEmailTemplate
    {
        public string ConfirmUrl { get; set; }

        public ConfirmEmailTemplate(string userName, string confirmUrl) : base(userName)
        {
            ConfirmUrl = confirmUrl;
        }

        public override string Subject => "[BloodLoop] Potwierdź email!";

        public override string GetContent()
        {
            return 
$@"Link do potwierdzenia: <a href=""{ConfirmUrl}"">Click!</a>

Cyaa";
        }
    }
}
