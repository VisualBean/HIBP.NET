using System.Collections.Generic;
using System.Threading.Tasks;
using HIBP.Responses;

namespace HIBP
{
    public interface IBreachApi
    {
        Breach GetBreach(string name);
        IEnumerable<Breach> GetBreaches(string domain = null, bool includeUnverified = false);
        IEnumerable<Breach> GetBreachesForAccount(string account, string domain = null, bool includeUnverified = false);
        Task<Breach> GetBreachAsync(string name);
        Task<IEnumerable<Breach>> GetBreachesAsync(string domain = null, bool includeUnverified = false);
        Task<IEnumerable<Breach>> GetBreachesForAccountAsync(string account, string domain = null, bool includeUnverified = false);
    }
}