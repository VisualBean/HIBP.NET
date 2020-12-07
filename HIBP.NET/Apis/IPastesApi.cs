namespace HIBP.Apis
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using HIBP.Responses;

    /// <summary>
    /// The interface for the pastes api.
    /// </summary>
    public interface IPastesApi
    {
        /// <summary>
        /// Gets the pastes asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A list of pastes.
        /// </returns>
        Task<IEnumerable<Paste>> GetPastesAsync(Email email, CancellationToken cancellationToken = default);
    }
}
