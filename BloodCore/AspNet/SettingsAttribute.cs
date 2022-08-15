using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.AspNet
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SettingsAttribute : Attribute
    {
        public string SectionName { get;}

        public SettingsAttribute(string sectionName)
        {
            SectionName = sectionName;
        }
    }
}
