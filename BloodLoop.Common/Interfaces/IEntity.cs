using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Domain
{
    public interface IEntity
    {

    }

    public interface IEntity<TIdentity> : IEntity where TIdentity : Identity
    {
        TIdentity Id { get; }
    }
}
