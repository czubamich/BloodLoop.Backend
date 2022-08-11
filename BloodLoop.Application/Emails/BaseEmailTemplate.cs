using BloodCore.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Emails
{
    public abstract class BaseEmailTemplate : EmailTemplate
    {
        private readonly string _userName;

        protected BaseEmailTemplate(string userName)
        {
            _userName = userName;
        }

        public override sealed string Print()
        {
            return
$@"[Nicely formatted document]
Cześć {_userName}!

{GetContent()}
[Nicely formatted]";
        }

        public abstract string GetContent();
    }
}
