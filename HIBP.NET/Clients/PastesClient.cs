namespace HIBP
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using HIBP.Responses;

    /// <summary>
    /// The 'Have I Been Pwned' Pastes API client.
    /// </summary>
    /// <seealso cref="ClientBase" />
    /// <seealso cref="IPastesClient" />
    public class PastesClient : ClientBase, IPastesClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PastesClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="serviceName">The service name.</param>
        public PastesClient(ApiKey apiKey, string serviceName)
            : base(apiKey, serviceName)
        {
        }

        /// <summary>
        /// Gets the pastes asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A list of pastes.
        /// </returns>
        public async Task<IEnumerable<Paste>> GetPastesAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<IEnumerable<Paste>>($"pasteaccount/{email}", cancellationToken);
        }
    }
}
