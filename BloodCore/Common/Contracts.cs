using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Common
{
    public static class Contracts
    {
        public static void Require(bool precondition, string message)
        {
            if (!precondition)
                throw new DomainException(message);
        }
    }
}
