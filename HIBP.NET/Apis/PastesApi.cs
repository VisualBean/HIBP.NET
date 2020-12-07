namespace HIBP.Apis
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using HIBP.Responses;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="HIBP.BaseApi" />
    /// <seealso cref="HIBP.Apis.IPastesApi" />
    public class PastesApi : BaseApi, IPastesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PastesApi"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="serviceName">The service name.</param>
        public PastesApi(ApiKey apiKey, string serviceName)
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
