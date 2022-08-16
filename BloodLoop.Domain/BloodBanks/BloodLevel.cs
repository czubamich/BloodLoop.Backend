using Ardalis.GuardClauses;
using BloodCore.Domain;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BloodLoop.Domain.BloodBanks
{
    public class BloodLevel : Entity<BloodLevelId>
    {
        public BloodBankId BloodBankId { get; private set; }

        public int Measurement { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public BloodType BloodType { get; private set; }

#region Constructors

        internal BloodLevel() { }

        internal BloodLevel(BloodLevelId id, BloodBankId bloodBankId, BloodType bloodType, int measurement, DateTime measuredAt) : base(id)
        {
            BloodBankId = bloodBankId;
            Measurement = measurement;
            CreatedAt = measuredAt;
            BloodType = bloodType;
        }

        #endregion

#region Creations

        public static BloodLevel Create(BloodBankId bloodBankId, BloodType bloodType, int measurement, DateTime measuredAt)
            => new BloodLevel(BloodLevelId.New, bloodBankId, bloodType, measurement, measuredAt);

#endregion

    }
}
