using BloodCore.Domain;
using System;

namespace BloodLoop.Domain.Staff
{
    public class StaffId : Identity<StaffId>
    {
        private StaffId(Guid id) : base(id)
        {
        }
    }
}
