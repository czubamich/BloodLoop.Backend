using BloodLoop.Domain.BloodBanks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodLoop.Application.Providers.RckikKatowice
{
    public interface IRckikKatowiceClient
    {
        Task<List<RK_BloodRS>> GetBloodLevels();
    }
}
