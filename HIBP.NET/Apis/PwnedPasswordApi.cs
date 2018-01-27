using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HIBP
{
    public class PwnedPasswordApi : BaseApi, IPwnedPasswordApi
    {
        /// <summary>
        /// The name of the client calling the API (used as user-agent).
        /// </summary>
        /// <param name="serviceName"></param>
        public PwnedPasswordApi(string serviceName) : base(serviceName)
        {
        }

        /// <summary>
        /// Returns wether or not a password has been part of a known breach.
        /// </summary>
        /// <param name="plainTextPasswordOrPasswordHash">Passwords can be plain text strings or a SHA1 hash of the password; HIBP will auto-detect the format and search accordingly.</param>
        /// <returns></returns>
        public async Task<bool> IsPasswordPwned(string plainTextPasswordOrPasswordHash)
        {
            var endpoint = $"pwnedpassword/{plainTextPasswordOrPasswordHash}";
            var response = await GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}
