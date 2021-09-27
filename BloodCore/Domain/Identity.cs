using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Domain
{
    public abstract class Identity : ValueObject, IEquatable<Identity>
    {
        public Guid Id { get; protected set; }

        protected Identity(Guid id) => Id = id;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString() => Id.ToString();

        public bool Equals(Identity other)
        {
            if (other is null) return false;
            return Id == other.Id;
        }

        public static implicit operator Guid(Identity id) => id.Id;
    }

    public abstract class Identity<TIdentity> : Identity where TIdentity : Identity
    {
        protected Identity(Guid id) : base(id) { }

        public static TIdentity Of(Guid id) => (TIdentity) Activator.CreateInstance
        (typeof(TIdentity), BindingFlags.NonPublic | BindingFlags.Instance, (Binder?) null, new object[] {id}, (CultureInfo?) null);

        public static TIdentity Of(string id) => Of(Guid.Parse(id));

        public static TIdentity New => Of(Guid.NewGuid());
    }
}
