namespace HIBP
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The PwnedPasswords API client interface.
    /// </summary>
    public interface IPwnedPasswordClient
    {
        /// <summary>
        /// Determines whether [is password pwned asynchronous] [the specified plain text password].
        /// </summary>
        /// <param name="plainTextOrHashedPassword">The plain text or pre-hashed password.</param>
        /// <param name="isHash">if set to <c>true</c> [is hash].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of times a password has shown up as 'pwned'.</returns>
        Task<int> IsPasswordPwnedAsync(string plainTextOrHashedPassword, bool isHash = false, CancellationToken cancellationToken = default);
    }
}