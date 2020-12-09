namespace HIBP
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using HIBP.Helpers;

    /// <summary>
    /// Base class for API usage.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public class ClientBase : IDisposable
    {
        /// <summary>
        /// The user agent header
        /// </summary>
        protected const string UserAgentHeader = "user-agent";

        /// <summary>
        /// The hibp API key header
        /// </summary>
        protected const string HIBPApiKeyHeader = "hibp-api-key";

        /// <summary>
        /// The internal http client.
        /// </summary>
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientBase" /> class.
        /// The name of the client calling the API (used as user-agent).
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="httpClient">The HTTP client.</param>
        /// <exception cref="ArgumentException">To interact with the HIBP API a name must be provided for the useragent string. This name is ment to be to distinguish your service from others. - serviceName.</exception>
        protected ClientBase(ApiKey apiKey, string serviceName, HttpClient httpClient = null)
            : this(httpClient, apiKey, serviceName)
        {
        }

        private ClientBase(HttpClient client, ApiKey apiKey, string serviceName)
        {
            if (apiKey == null)
            {
                throw new InvalidApiKeyException();
            }

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new InvalidServiceNameException();
            }

            if (client == null)
            {
                this.client = new HttpClient();
            }
            else
            {
                this.client = client;
            }

            this.client.BaseAddress = new Uri("https://haveibeenpwned.com/api/v3/");
            this.SetMandatoryDefaultRequestHeaders(apiKey, serviceName);
        }

        /// <summary>
        /// Sets the mandatory request headers.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="serviceName">Name of the service.</param>
        protected void SetMandatoryDefaultRequestHeaders(ApiKey apiKey, string serviceName)
        {
            this.client.DefaultRequestHeaders.Add(UserAgentHeader, serviceName);
            this.client.DefaultRequestHeaders.Add(HIBPApiKeyHeader, apiKey.Key);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.client.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Basic GetAsync T.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response data into.</typeparam>
        /// <param name="requestUri">The requestUri to call.</param>
        /// <param name="cancellationToken">The cancellationToken.</param>
        /// <returns>
        /// a <see cref="Task{T}" />.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// ApiKey is not valid.
        /// or
        /// User-Agent is not valid.
        /// </exception>
        /// <exception cref="System.Exception">Your request has been throttled, please try again later.</exception>
        protected async Task<T> GetAsync<T>(string requestUri, CancellationToken cancellationToken)
        {
            var response = await this.GetAsync(requestUri, cancellationToken);
            return await response.Content.ReadAsJsonAsync<T>();
        }

        /// <summary>
        /// Makes an async GET request using the HTTPClient.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// a <see cref="HttpRequestMessage" />.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// ApiKey is not valid.
        /// or
        /// User-Agent is not valid.
        /// </exception>
        /// <exception cref="System.Exception">Your request has been throttled, please try again later.</exception>
        protected async Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
        {
            var response = await this.client.GetAsync(requestUri, cancellationToken);
            switch (response.StatusCode)
            {
                case (HttpStatusCode)401:
                {
                    throw new InvalidApiKeyException();
                }

                case (HttpStatusCode)403:
                {
                    throw new InvalidServiceNameException();
                }

                case (HttpStatusCode)429:
                {
                    throw new TooManyRequestsException();
                }
            }

            return response;
        }
    }
}