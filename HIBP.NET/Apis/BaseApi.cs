using HIBP.Extensions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HIBP
{
    public class BaseApi : IDisposable
    {
        private BaseApi()
        {
        }

        protected readonly HttpClient client;
        private readonly CancellationTokenSource cancellationTokenSource;
        protected readonly CancellationToken cancellationToken;

        /// <summary>
        /// The name of the client calling the API (used as user-agent).
        /// </summary>
        /// <param name="serviceName"></param>
        protected BaseApi(string apiKey, string serviceName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (serviceName.IsNullEmptyOrWhitespace())
            {
                throw new ArgumentException("To interact with the HIBP API a name must be provided for the useragent string. This name is ment to be to distinguish your service from others.", nameof(serviceName));
            }

            if (apiKey.IsNullEmptyOrWhitespace())
            {
                throw new ArgumentException("To interact with the HIBP API a valid apikey must be provided. You can get it here: https://haveibeenpwned.com/API/Key", nameof(apiKey));
            }

            if (!cancellationToken.CanBeCanceled)
            {
                cancellationTokenSource = new CancellationTokenSource();
                cancellationToken = cancellationTokenSource.Token;
            }

            client = new HttpClient()
            {
                BaseAddress = new Uri("https://haveibeenpwned.com/api/v3/"),                
            };

            client.DefaultRequestHeaders.Add("user-agent", serviceName);
            client.DefaultRequestHeaders.Add("hibp-api-key", apiKey);
        }
    
        /// <summary>
        /// Basic GetAsync T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <returns><see cref="Task{T}"/></returns>
        protected async Task<T> GetAsync<T>(string requestUri)
        {
            var response = await client.GetAsync(requestUri, cancellationToken);
            ThrowOnErrorResponse(response.StatusCode);

            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return default(T);
            }

            return await response.Content.ReadAsJsonAsync<T>();
        }

        protected async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            var response = await client.GetAsync(requestUri, cancellationToken);
            ThrowOnErrorResponse(response.StatusCode);
            return response;
        }

        public void Dispose()
        {
            if(cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
            
            client.Dispose();
            GC.SuppressFinalize(this);
        }

        private void ThrowOnErrorResponse(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case (HttpStatusCode)401:
                    throw new ArgumentException("ApiKey is not valid.");
                case (HttpStatusCode)403:
                    throw new ArgumentException("User-Agent is not valid.");
                case (HttpStatusCode)429:
                    throw new Exception("Your request has been throttled, please try again later");
            }
        }
    }
}
