namespace HIBP
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using HIBP.Responses;

    /// <summary>
    /// The Breach API Interface.
    /// </summary>
    public interface IBreachApi
    {
        /// <summary>
        /// Gets the breach asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <see cref="Breach" /> if a breach of that name could be found
        /// </returns>
        Task<Breach> GetBreachAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the breaches asynchronous.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="includeUnverified">if set to <c>true</c> [include unverified].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A list of <see cref="Breach" />.
        /// </returns>
        Task<IEnumerable<Breach>> GetBreachesAsync(string domain = null, bool includeUnverified = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the breaches for account asynchronous.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="truncateResponse">if set to <c>true</c> [truncate response].</param>
        /// <param name="domain">The domain.</param>
        /// <param name="includeUnverified">if set to <c>true</c> [include unverified].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A list of <see cref="Breach" />.
        /// </returns>
        Task<IEnumerable<Breach>> GetBreachesForAccountAsync(string account, bool truncateResponse = false, string domain = null, bool includeUnverified = false, CancellationToken cancellationToken = default);
    }
}