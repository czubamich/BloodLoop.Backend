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
        public string UserName { get; set; }

        protected BaseEmailTemplate(string userName)
        {
            UserName = userName;
        }

        public override sealed string Print()
        {
            return
$@"
<!DOCTYPE html>
<html lang=""pl"">
<body>
Cześć {UserName}!</br>
</br>
{GetContent().Replace("\n", "\n</br>")}
</body>
</html>";
        }

        public abstract string GetContent();
    }
}
