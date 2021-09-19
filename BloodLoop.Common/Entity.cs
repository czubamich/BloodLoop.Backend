using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Domain
{
    public abstract class Entity<TIdentity> : IEquatable<Entity<TIdentity>>, IEntity<TIdentity> where TIdentity : Identity
    {
        public TIdentity Id { get; protected set; }

        protected Entity(TIdentity id) => this.Id = id;

        protected Entity() { }



        public bool Equals(Entity<TIdentity> other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType()) return false;

            return Equals((Entity<TIdentity>)obj);
        }

        public override int GetHashCode() => this.GetType().GetHashCode() * 907 + this.Id.GetHashCode();

        public override string ToString() => $"{this.GetType().Name}#[Identity={this.Id}]";

        public static bool operator ==(Entity<TIdentity> a, Entity<TIdentity> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TIdentity> a, Entity<TIdentity> b)
        {
            return !(a == b);
        }
    }
}
