using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HIBP
{
    /// <summary>
    /// The Have I Been Pwned PwnedPassword API Wrapper
    /// </summary>
    /// <seealso cref="HIBP.BaseApi" />
    /// <seealso cref="HIBP.IPwnedPasswordApi" />
    public sealed class PwnedPasswordApi : BaseApi, IPwnedPasswordApi
    {
        /// <summary>
        /// Initializes the PwnedPassword API with a <paramref name="serviceName"/>
        /// </summary>
        /// <param name="serviceName">The name of the client calling the API (used as user-agent).</param>
        public PwnedPasswordApi(string serviceName) : base(serviceName)
        {
        }
        /// <summary>
        /// Determines whether [is password pwned] [the specified plain text password or password hash].
        /// </summary>
        /// <param name="plainTextPasswordOrPAsswordHash">The plain text password or password hash.</param>
        /// <returns>
        ///   <c>true</c> if password has been pwned; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPasswordPwned(string plainTextPasswordOrPAsswordHash)
        {
            return Task.Run(() => IsPasswordPwnedAsync(plainTextPasswordOrPAsswordHash)).Result;
        }
        /// <summary>
        /// Determines whether [is password pwned asynchronous] [the specified plain text password or password hash].
        /// </summary>
        /// <param name="plainTextPasswordOrPasswordHash">The plain text password or password hash.</param>
        /// <returns>
        ///  <c>true</c> if password has been pwned; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> IsPasswordPwnedAsync(string plainTextPasswordOrPasswordHash)
        {
            if (string.IsNullOrEmpty(plainTextPasswordOrPasswordHash))
                throw new ArgumentNullException("plainTextPasswordOrPasswordHash");

            var endpoint = $"pwnedpassword/{plainTextPasswordOrPasswordHash}";
            var response = await GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}
