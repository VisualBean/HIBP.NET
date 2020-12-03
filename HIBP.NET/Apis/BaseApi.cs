namespace HIBP
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using HIBP.Extensions;

    /// <summary>
    /// Base class for API usage.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public class BaseApi : IDisposable
    {
        protected readonly HttpClient Client;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApi" /> class.
        /// The name of the client calling the API (used as user-agent).
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="serviceName">The service name.</param>
        /// <exception cref="ArgumentException">To interact with the HIBP API a name must be provided for the useragent string. This name is ment to be to distinguish your service from others. - serviceName</exception>
        protected BaseApi(ApiKey apiKey, string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentException("To interact with the HIBP API a name must be provided for the useragent string. This name is ment to be to distinguish your service from others.", nameof(serviceName));
            }

            this.Client = new HttpClient()
            {
                BaseAddress = new Uri("https://haveibeenpwned.com/api/v3/"),
            };

            this.Client.DefaultRequestHeaders.Add("user-agent", serviceName);
            this.Client.DefaultRequestHeaders.Add("hibp-api-key", apiKey.Key);
        }

        private BaseApi()
        {
        }

        public void Dispose()
        {
            this.Client.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>Basic GetAsync T.</summary>
        /// <typeparam name="T">The type to deserialize the response data into.</typeparam>
        /// <param name="requestUri">The requestUri to call.</param>
        /// <param name="cancellationToken">The cancellationToken.</param>
        /// <returns>a <see cref="Task{T}" />.</returns>
        protected async Task<T> GetAsync<T>(string requestUri, CancellationToken cancellationToken)
        {
            var response = await this.Client.GetAsync(requestUri, cancellationToken);
            this.ThrowOnErrorResponse(response);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            return await response.Content.ReadAsJsonAsync<T>();
        }

        /// <summary>
        /// Makes an async GET request using the HTTPClient.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>a <see cref="HttpRequestMessage"/>.</returns>
        protected async Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
        {
            var response = await this.Client.GetAsync(requestUri, cancellationToken);
            this.ThrowOnErrorResponse(response);
            return response;
        }

        private void ThrowOnErrorResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case (HttpStatusCode)401:
                {
                    throw new ArgumentException("ApiKey is not valid.");
                }

                case (HttpStatusCode)403:
                {
                    throw new ArgumentException("User-Agent is not valid.");
                }

                case (HttpStatusCode)429:
                {
                    throw new Exception("Your request has been throttled, please try again later");
                }
            }
        }
    }
}