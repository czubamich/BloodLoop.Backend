using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Domain
{
    public abstract class Identity : ValueObject
    {
        public Guid Id { get; protected set; }

        protected Identity(Guid id) => Id = id;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString() => Id.ToString();

        public static implicit operator Guid(Identity id) => id.Id;
    }
}
