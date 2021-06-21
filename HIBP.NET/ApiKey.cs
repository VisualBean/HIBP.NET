namespace HIBP
{
    using System;

    /// <summary>
    /// The ApiKey class.
    /// </summary>
    public class ApiKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKey"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <exception cref="ArgumentException">To interact with the HIBP API a valid apikey must be provided. You can get it here: https://haveibeenpwned.com/API/Key - apiKey.</exception>
        public ApiKey(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("To interact with the HIBP API a valid apikey must be provided. You can get it here: https://haveibeenpwned.com/API/Key.", nameof(apiKey));
            }

            this.Key = apiKey;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        internal string Key { get; }
    }
}
