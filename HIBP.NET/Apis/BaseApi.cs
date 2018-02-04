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
        protected BaseApi(string serviceName)
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            client = new HttpClient
            {
                BaseAddress = new Uri("https://haveibeenpwned.com/api/v2/")
            };
            client.DefaultRequestHeaders.Add("user-agent", serviceName);
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
            switch (response.StatusCode)
            {
                case (HttpStatusCode)429:
                    throw new Exception("Your request has been throttled, please try again later");
                case HttpStatusCode.NotFound:
                    return default(T);
            }
            return await response.Content.ReadAsJsonAsync<T>();
        }
        protected async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            var response = await client.GetAsync(requestUri, cancellationToken);
            switch (response.StatusCode)
            {
                case (HttpStatusCode)429:
                    throw new Exception("Your request has been throttled, please try again later");
            }
            return response;
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            client.Dispose();

        }
    }
}
