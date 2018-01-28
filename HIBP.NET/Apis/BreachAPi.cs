using HIBP.Extensions;
using HIBP.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HIBP
{
    public class BreachApi : BaseApi, IBreachApi
    {
        /// <summary>
        /// The name of the client calling the API (used as user-agent).
        /// </summary>
        /// <param name="serviceName"></param>
        public BreachApi(string serviceName) : base(serviceName)
        {
        }

        /// <summary>
        /// Returns a breach by name.
        /// </summary>
        /// <param name="domain">optional domain parameter, if you only want breaches for a certain domain. example: adobe.com</param>
        /// <param name="includeUnverified">Include unverified breaches.</param>
        /// <returns></returns>
        public async Task<Breach> GetBreachAsync(string name)
        {
            var endpoint = $"breaches/{name}";

           return await GetAsync<Breach>(endpoint);
        }
        /// <summary>
        /// Returns all breaches.
        /// </summary>
        /// <param name="domain">optional domain parameter, if you only want breaches for a certain domain. example: adobe.com</param>
        /// <param name="includeUnverified">Include unverified breaches.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Breach>> GetBreachesAsync(string domain = null, bool includeUnverified = false)
        {
            var endpoint = $"breaches?includeUnverified={includeUnverified.ToBooleanString()}";
            if (domain != null)
                endpoint += $"&domain={domain}";

            return await GetAsync<IEnumerable<Breach>>(endpoint);
        }
        /// <summary>
        /// Returns breaches for a certain account.
        /// Returns an empty list if no breaches were found for account.
        /// </summary>
        /// <param name="account">account for which you want breaches</param>
        public async Task<IEnumerable<Breach>> GetBreachesForAccountAsync(string account, string domain = null, bool includeUnverified = false)
        {
            var _account = System.Web.HttpUtility.UrlEncode(account);
            var endpoint = $"breachedaccount/{_account}/?includeUnverified={includeUnverified.ToBooleanString()}";
            if (domain != null)
                endpoint += $"&domain={domain}";

            var result = await GetAsync<IEnumerable<Breach>>(endpoint);
            if (result == null)
                return new List<Breach>();

            return result;
        }
    }
}
