using BloodCore.Common;
using System;

namespace BloodLoop.Domain.Accounts
{
    public class RoleId : Identity
    {
        private RoleId(Guid id) : base(id) { }

        public static RoleId Of(string id) => Of(new Guid(id));

        public static RoleId Of(Guid id) => new RoleId(id);

        public static RoleId New => new RoleId(Guid.NewGuid());
    }
}
