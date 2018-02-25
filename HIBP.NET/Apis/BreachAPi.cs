using HIBP.Extensions;
using HIBP.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HIBP
{
    /// <summary>
    /// The Have I Been Pwned Breach API Wrapper.
    /// </summary>
    /// <seealso cref="HIBP.BaseApi" />
    /// <seealso cref="HIBP.IBreachApi" />
    public sealed class BreachApi : BaseApi, IBreachApi
    {
        /// <summary>
        /// Initializes the BreachApi with a <paramref name="serviceName"/>
        /// </summary>
        /// <param name="serviceName"> The name of the client calling the API (used as user-agent).</param>
        public BreachApi(string serviceName) : base(serviceName)
        {
        }

        /// <summary>
        /// Gets a breach by <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the breach</param>
        /// <returns>
        /// <see cref="Breach"/> if a breach of that name could be found
        /// </returns>
        public Breach GetBreach(string name)
        {
            return Task.Run(() => GetBreachAsync(name)).Result;
        }
        /// <summary>
        /// Gets all breaches.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="includeUnverified">if set to <c>true</c> [include unverified].</param>
        /// <returns>
        /// A list of <see cref="Breach"/>
        /// </returns>
        public IEnumerable<Breach> GetBreaches(string domain = null, bool includeUnverified = false)
        {
            return Task.Run(() => GetBreachesAsync(domain, includeUnverified)).Result;
        }
        /// <summary>
        /// Gets all breaches for account.
        /// </summary>
        /// <param name="account">The account name</param>
        /// <param name="domain">The domain name</param>
        /// <param name="includeUnverified">if set to <c>true</c> [include unverified].</param>
        /// <returns>
        /// A list of <see cref="Breach"/>
        /// </returns>
        public IEnumerable<Breach> GetBreachesForAccount(string account, string domain = null, bool includeUnverified = false)
        {
            return Task.Run(() => GetBreachesForAccountAsync(account, domain, includeUnverified)).Result;
        }
        /// <summary>
        /// Gets the breach asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// <see cref="Breach"/> if a breach of that name could be found
        /// </returns>
        public async Task<Breach> GetBreachAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            var endpoint = $"breaches/{name}";

            return await GetAsync<Breach>(endpoint);
        }
        /// <summary>
        /// Gets all breaches asynchronous.
        /// </summary>
        /// <param name="domain">The domain name.</param>
        /// <param name="includeUnverified">if set to <c>true</c> [include unverified].</param>
        /// <returns>
        /// A list of <see cref="Breach"/>
        /// </returns>
        public async Task<IEnumerable<Breach>> GetBreachesAsync(string domain = null, bool includeUnverified = false)
        {
            var endpoint = $"breaches?includeUnverified={includeUnverified.ToBooleanString()}";
            if (domain != null)
                endpoint += $"&domain={domain}";

            return await GetAsync<IEnumerable<Breach>>(endpoint);
        }
        /// <summary>
        /// Gets the breaches for account asynchronous.
        /// </summary>
        /// <param name="account">The account name.</param>
        /// <param name="domain">The domain name.</param>
        /// <param name="includeUnverified">if set to <c>true</c> [include unverified].</param>
        /// <returns>
        /// A list of <see cref="Breach"/>
        /// </returns>
        public async Task<IEnumerable<Breach>> GetBreachesForAccountAsync(string account, string domain = null, bool includeUnverified = false)
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account");

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
