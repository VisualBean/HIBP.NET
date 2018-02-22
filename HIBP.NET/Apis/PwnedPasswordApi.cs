using HIBP.Extensions;
using HIBP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
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
            client.BaseAddress = new Uri("https://api.pwnedpasswords.com/");
        }
        /// <summary>
        /// Determines whether [is password pwned] [the specified plain text password or password hash].
        /// </summary>
        /// <param name="plainTextPasswordOrPAsswordHash">The plain text password or password hash.</param>
        /// <returns>
        ///  Count of times password has been seen. <c>0</c> if password has not been pwned.
        /// </returns>
        public int IsPasswordPwned(string plainTextPasswordOrPAsswordHash)
        {
            return Task.Run(() => IsPasswordPwnedAsync(plainTextPasswordOrPAsswordHash)).Result;
        }
        /// <summary>
        /// Determines whether [is password pwned asynchronous] [the specified plain text password or password hash].
        /// </summary>
        /// <param name="plainTextPasswordOrPasswordHash">The plain text password or password hash.</param>
        /// <returns>
        ///  Count of times password has been seen. <c>0</c> if password has not been pwned.
        /// </returns>
        public async Task<int> IsPasswordPwnedAsync(string plainTextPasswordOrPasswordHash)
        {
            if (string.IsNullOrEmpty(plainTextPasswordOrPasswordHash))
                throw new ArgumentNullException("plainTextPasswordOrPasswordHash");

            var endpoint = $"pwnedpassword/{plainTextPasswordOrPasswordHash}";
            var response = await GetAsync<int>(endpoint);
            return response;
        }
        public int IsPasswordPwnedSafe(string plainTextPassword)
        {
            return Task.Run(() => IsPasswordPwnedSafeAsync(plainTextPassword)).Result;
        }
        /// <summary>
        /// Performs searching in memory instead of sending the entire password/sha1 over the wire.
        /// Will SHA1 the password internally
        /// </summary>
        /// <param name="plainTextPassword">PlainText Password</param>
        /// <returns>
        /// Amount of times the password has been seen in breaches <c>0</c> if not seen. 
        /// </returns>
        public async Task<int> IsPasswordPwnedSafeAsync(string plainTextPassword)
        {
            if (string.IsNullOrEmpty(plainTextPassword))
                throw new ArgumentNullException("plainTextPassword");

            var sha1 = plainTextPassword.ToSHA1();
            var First5 = sha1.Substring(0, 5);
            var endpoint = $"range/{First5}";
            var response = await GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var hashString = await response.Content.ReadAsStringAsync();
                var hashes = hashString.Split(Environment.NewLine);
                var found = hashes.FirstOrDefault(r => $"{First5}{r}".Contains(sha1));
                if (found == null)
                    return 0;
                return Convert.ToInt32(found.Split(':')[1]);
            }
            else
            {
                return 0;
            }
            
        }
    }
}
