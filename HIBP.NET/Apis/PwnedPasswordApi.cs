namespace HIBP
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using HIBP.Extensions;

    /// <summary>
    /// The Have I Been Pwned PwnedPassword API Wrapper
    /// </summary>
    /// <seealso cref="HIBP.BaseApi" />
    /// <seealso cref="HIBP.IPwnedPasswordApi" />
    public sealed class PwnedPasswordApi : BaseApi, IPwnedPasswordApi
    {
        private static readonly ApiKey ApiKey = new ApiKey("N/A");

        /// <summary>
        /// Initializes a new instance of the <see cref="PwnedPasswordApi"/> class.
        /// </summary>
        /// <param name="serviceName">The name of the client calling the API (used as user-agent).</param>
        public PwnedPasswordApi(string serviceName)
            : base(ApiKey, serviceName)
        {
            this.Client.BaseAddress = new Uri("https://api.pwnedpasswords.com/");
        }

        /// <summary>
        /// Determines whether [is password pwned asynchronous] [the specified plain text password].
        /// </summary>
        /// <param name="plainTextPassword">The plain text password.</param>
        /// <param name="isHash">if set to <c>true</c> [is hash].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The number of times a password has shown up as 'pwned'
        /// </returns>
        /// <exception cref="ArgumentNullException">plainTextPassword</exception>
        /// <remark>
        /// Performs searching in memory instead of sending the entire password/sha1 over the wire.
        /// Will SHA1 the password internally
        /// </remark>
        public async Task<int> IsPasswordPwnedAsync(string plainTextPassword, bool isHash = false, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(plainTextPassword))
            {
                throw new ArgumentNullException(nameof(plainTextPassword));
            }

            string sha1 = plainTextPassword;

            if (!isHash)
            {
                sha1 = plainTextPassword.ToSHA1();
            }

            var response = await this.GetAsync($"range/{sha1.First5()}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var hashString = await response.Content.ReadAsStringAsync();
                return this.ReduceResult(sha1, hashString);
            }
            else
            {
                return 0;
            }
        }

        private int ReduceResult(string hashToFind, string listOfHashes)
        {
            var hashes = listOfHashes.Split(Environment.NewLine);
            var first5 = hashToFind.First5();
            var found = hashes.FirstOrDefault(h => $"{first5}{h}".Contains(hashToFind));
            if (found == null)
            {
                return 0;
            }

            return Convert.ToInt32(found.Split(':')[1]);
        }
    }
}