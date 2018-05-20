using HIBP.Extensions;
using HIBP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public PwnedPasswordApi(string serviceName, CancellationToken cancellationToken = default(CancellationToken)) : base(serviceName, cancellationToken)
        {
            client.BaseAddress = new Uri("https://api.pwnedpasswords.com/");
        }


        public int IsPasswordPwned(string plainTextPassword)
        {
            return Task.Run(() => IsPasswordPwnedAsync(plainTextPassword)).Result;
        }

        /// <summary>
        /// Performs searching in memory instead of sending the entire password/sha1 over the wire.
        /// Will SHA1 the password internally
        /// </summary>
        /// <param name="plainTextPassword">PlainText Password</param>
        /// <returns>
        /// Amount of times the password has been seen in breaches. <c>0</c> if not seen. 
        /// </returns>
        public async Task<int> IsPasswordPwnedAsync(string plainTextPassword)
        {
            if (string.IsNullOrEmpty(plainTextPassword))
                throw new ArgumentNullException("plainTextPassword");

            var sha1 = plainTextPassword.ToSHA1();
            var endpoint = $"range/{sha1.First5()}";
            var response = await GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var hashString = await response.Content.ReadAsStringAsync();
                return ReduceResult(sha1, hashString);
            }
            else
            {
                return 0;
            }
        }

        private int ReduceResult(string hashToFind,string listOfHashes)
        {
            var hashes = listOfHashes.Split(Environment.NewLine);
            var first5 = hashToFind.First5();
            var found = hashes.FirstOrDefault(h => $"{first5}{h}".Contains(hashToFind));
            if (found == null)
                return 0;
            return Convert.ToInt32(found.Split(':')[1]);
        }
    }
}
