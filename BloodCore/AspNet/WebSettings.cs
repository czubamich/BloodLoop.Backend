using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.AspNet
{
    [Settings(nameof(WebSettings))]
    public class WebSettings
    {
        public string WebUrl { get; set; }
        public string ApiUrl { get; set; }
        public bool HttpsEnabled { get; set; }
    }
}
