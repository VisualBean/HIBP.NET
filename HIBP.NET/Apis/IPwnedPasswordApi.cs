namespace HIBP
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPwnedPasswordApi
    {
        /// <summary>
        /// Determines whether [is password pwned asynchronous] [the specified plain text password].
        /// </summary>
        /// <param name="plainTextPassword">The plain text password.</param>
        /// <param name="isHash">if set to <c>true</c> [is hash].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of times a password has shown up as 'pwned'</returns>
        Task<int> IsPasswordPwnedAsync(string plainTextPassword, bool isHash = false, CancellationToken cancellationToken = default);
    }
}