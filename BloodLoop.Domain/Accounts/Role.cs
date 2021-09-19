using BloodCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Accounts
{
    public class Role : ValueObject
    {
        public RoleId Id { get; private set; }
        public string Name { get; private set; }

        private Role(RoleId id, string name)
        {
            Id = id;
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        #region Defaults

        public static Role Donor => new Role(RoleId.Of(new Guid("20e163e1-a1a2-431d-9d6a-3ba4fadbe593")), "Donor");
        public static Role Staff => new Role(RoleId.Of(new Guid("b1c362fd-095d-41e3-8050-c3f622ee73eb")), "Staff");
        public static Role Admin => new Role(RoleId.Of(new Guid("1075a2e7-2722-48f0-ba13-826e9a84de09")), "Admin");

        #endregion
    }
}
