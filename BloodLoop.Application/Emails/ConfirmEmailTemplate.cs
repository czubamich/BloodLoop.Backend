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
        private readonly string _confirmToken;

        public ConfirmEmailTemplate(string userName, string confirmToken) : base(userName)
        {
            _confirmToken = confirmToken;
        }

        public override string GetContent()
        {
            return $@"Twój token potwierdzający: {_confirmToken}";
        }
    }
}
