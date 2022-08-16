using BloodCore.Domain;

namespace BloodLoop.Domain.BloodBanks
{
    public class BloodLevelAddedEvent : IDomainEvent
    {
        public BloodLevel NewBloodLevel { get; set; }

        public BloodLevelAddedEvent(BloodLevel bloodLevel)
        {
           NewBloodLevel = bloodLevel;
        }
    }

}
