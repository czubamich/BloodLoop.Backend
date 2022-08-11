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
        private readonly string _resetToken;

        public ForgotPasswordEmailTemplate(string userName, string resetToken) : base(userName)
        {
            _resetToken = resetToken;
        }

        public override string GetContent()
        {
            return $@"Twój token do resetu: {_resetToken}";
        }
    }
}
