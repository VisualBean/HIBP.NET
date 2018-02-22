using System.Collections.Generic;
using System.Threading.Tasks;
using HIBP.Responses;

namespace HIBP
{
    public interface IBreachApi
    {
        BreachResponse GetBreach(string name);
        IEnumerable<BreachResponse> GetBreaches(string domain = null, bool includeUnverified = false);
        IEnumerable<BreachResponse> GetBreachesForAccount(string account, string domain = null, bool includeUnverified = false);
        Task<BreachResponse> GetBreachAsync(string name);
        Task<IEnumerable<BreachResponse>> GetBreachesAsync(string domain = null, bool includeUnverified = false);
        Task<IEnumerable<BreachResponse>> GetBreachesForAccountAsync(string account, string domain = null, bool includeUnverified = false);
    }
}