using BloodCore.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Emails
{
    public class ForgotPasswordEmailTemplate : BaseEmailTemplate
    {
        public string ResetUrl { get; set; }

        public ForgotPasswordEmailTemplate(string userName, string resetLink) : base(userName)
        {
            ResetUrl = resetLink;
        }

        public override string Subject => "[BloodLoop] Zapomniane hasło";

        public override string GetContent()
        {
            return $@"Twój link do resetu: <a href=""{ResetUrl}"">Click!</a>";
        }
    }
}
