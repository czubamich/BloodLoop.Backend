﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Common
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

        public static TIdentity Of(Guid id) => (TIdentity)Activator.CreateInstance(typeof(TIdentity), new[] { id } );

        public static TIdentity Of(string id) => Of(new Guid(id));

        public static TIdentity New => (TIdentity)Activator.CreateInstance(typeof(TIdentity), new[] { Guid.NewGuid() });
    }
}
