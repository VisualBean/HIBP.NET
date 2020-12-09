using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HIBP.Tests
{
    [TestClass]
    public class ClientBaseTests : ClientBase
    {

        private static ApiKey validKey = new ApiKey("123");
        private static string serviceName = "service";

        public ClientBaseTests() : base(validKey, serviceName, null)
        {

        }

        public ClientBaseTests(ApiKey key,  string serviceName, HttpClient client) : base(key, serviceName, client)
        {

        }

        private HttpMessageHandler SetupHttpHandler(HttpResponseMessage responseMessage)
        {
            Mock<HttpMessageHandler> mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            return mockHandler.Object;
        }


        [TestMethod]
        public async Task GetAsync_WithKnownErrorResponse_Throws()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.Content = new StringContent("", Encoding.UTF8, "application/json");

            var httpClient = new HttpClient(SetupHttpHandler(httpResponse));
            var client = new ClientBaseTests(validKey, serviceName, httpClient);

            httpResponse.StatusCode = HttpStatusCode.Unauthorized;
            await Assert.ThrowsExceptionAsync<InvalidApiKeyException>(() => client.GetAsync("", default));
            
            httpResponse.StatusCode = HttpStatusCode.Forbidden;
            await Assert.ThrowsExceptionAsync<InvalidServiceNameException>(() => client.GetAsync("", default));

            httpResponse.StatusCode = HttpStatusCode.TooManyRequests;
            await Assert.ThrowsExceptionAsync<TooManyRequestsException>(() => client.GetAsync("", default));
        }


        [TestMethod]
        public async Task GetAsyncT_WithKnownErrorResponse_Throws()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.Content = new StringContent("", Encoding.UTF8, "application/json");

            var httpClient = new HttpClient(SetupHttpHandler(httpResponse));
            var client = new ClientBaseTests(validKey, serviceName, httpClient);

            httpResponse.StatusCode = HttpStatusCode.Unauthorized;
            await Assert.ThrowsExceptionAsync<InvalidApiKeyException>(() => client.GetAsync<string>("", default));

            httpResponse.StatusCode = HttpStatusCode.Forbidden;
            await Assert.ThrowsExceptionAsync<InvalidServiceNameException>(() => client.GetAsync<string>("", default));

            httpResponse.StatusCode = HttpStatusCode.TooManyRequests;
            await Assert.ThrowsExceptionAsync<TooManyRequestsException>(() => client.GetAsync<string>("", default));

        }

        [TestMethod]
        public void ClientBase_InternalHttpClient_HasMandatoryHeaders()
        {
            var httpClient = new HttpClient();
            _ = new ClientBaseTests(validKey, serviceName, httpClient);

            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains(UserAgentHeader));
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains(HIBPApiKeyHeader));
        }
    }
}
