namespace HIBP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using HIBP.Helpers;
    using HIBP.Responses;

    /// <summary>
    /// The 'Have I Been Pwned' Breach API client.
    /// </summary>
    /// <seealso cref="ClientBase" />
    /// <seealso cref="IBreachClient" />
    public sealed class BreachClient : ClientBase, IBreachClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreachClient"/> class.
        /// </summary>
        /// <param name="apiKey">The apikey for accessing the HIBP API.</param>
        /// <param name="serviceName"> The name of the client calling the API (used as user-agent).</param>
        /// <param name="client">The client. If none is provided, a new one will be created.</param>
        public BreachClient(ApiKey apiKey, string serviceName, HttpClient client = null)
            : base(apiKey, serviceName, client)
        {
        }

        /// <summary>
        /// Gets the breach asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Breach" /> if a breach of that name could be found.</returns>
        /// <exception cref="ArgumentNullException">name.</exception>
        public async Task<Breach> GetBreachAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return await this.GetAsync<Breach>($"breaches/{name}", cancellationToken);
        }

        /// <summary>
        /// Gets all breaches asynchronous.
        /// </summary>
        /// <param name="domain">The domain name.</param>
        /// <param name="includeUnverified">if set to <c>true</c> [include unverified].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A list of <see cref="Breach" />.
        /// </returns>
        public async Task<IEnumerable<Breach>> GetBreachesAsync(string domain = null, bool includeUnverified = false, CancellationToken cancellationToken = default)
        {
            var endpoint = $"breaches?includeUnverified={includeUnverified.ToBooleanString()}";
            if (domain != null)
            {
                endpoint += $"&domain={domain}";
            }

            return await this.GetAsync<IEnumerable<Breach>>(endpoint, cancellationToken) ?? Enumerable.Empty<Breach>();
        }

        /// <summary>
        /// Gets the breaches for account asynchronous.
        /// </summary>
        /// <param name="account">The account name.</param>
        /// <param name="truncateResponse">if set to <c>true</c> [truncate response].</param>
        /// <param name="domain">The domain name.</param>
        /// <param name="includeUnverified">if set to <c>true</c> [include unverified].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A list of <see cref="Breach" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">account.</exception>
        public async Task<IEnumerable<Breach>> GetBreachesForAccountAsync(
            string account,
            bool truncateResponse = false,
            string domain = null,
            bool includeUnverified = false,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                throw new ArgumentNullException(nameof(account));
            }

            var urlEncodedAccount = System.Web.HttpUtility.UrlEncode(account);
            var endpoint = $"breachedaccount/{urlEncodedAccount}/?truncateResponse={truncateResponse.ToBooleanString()}&includeUnverified={includeUnverified.ToBooleanString()}";
            if (domain != null)
            {
                endpoint += $"&domain={domain}";
            }

            return await this.GetAsync<IEnumerable<Breach>>(endpoint, cancellationToken) ?? Enumerable.Empty<Breach>();
        }
    }
}