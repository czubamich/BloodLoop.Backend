using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class InjectableAttribute : Attribute
    {
        public InjectableAttribute(params Type[] contracts)
        {
            if (contracts.Any())
                Contracts = contracts;
        }

        public Type[] Contracts { get; }
        public bool IsScoped { get; set; }
    }
}
