
namespace HIBP.Tests
{
    using HIBP.Helpers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class InjectionExtensionTests
    {
        private static ApiKey validKey = new ApiKey("abc");
        private static string validServiceName = "test";

        [TestMethod]
        [DataRow(typeof(IPwnedPasswordClient))]
        [DataRow(typeof(IBreachClient))]
        [DataRow(typeof(IPastesClient))]
        public void AddHIBP_AddsAllClient(Type type)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHIBP(c => 
            {
                c.ApiKey = validKey;
                c.ServiceName = validServiceName;
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService(type);

            Assert.IsNotNull(service);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidApiKeyException))]
        public void AddHIBP_WithoutApiKey_Throws()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHIBP(c =>
            {
                c.ServiceName = validServiceName;
            });

            var _ = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(InvalidServiceNameException))]
        public void AddHIBP_WithInvalidServiceName_Throws(string serviceName)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHIBP(c =>
            {
                c.ServiceName = serviceName;
                c.ApiKey = validKey;
            });

            var _ = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidApiKeyException))]
        public void AddBreachClient_WithoutApiKey_Throws()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBreachClient(null, validServiceName);

            var _ = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(InvalidServiceNameException))]
        public void AddBreachClient_WithInvalidServiceName_Throws(string serviceName)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBreachClient(validKey, serviceName);

            var _ = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidApiKeyException))]
        public void AddPastesClient_WithoutApiKey_Throws()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPastesClient(null, validServiceName);

            var _ = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(InvalidServiceNameException))]
        public void AddPastesClient_WithInvalidServiceName_Throws(string serviceName)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPastesClient(validKey, serviceName);

            var _ = serviceCollection.BuildServiceProvider();
        }


        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(InvalidServiceNameException))]
        public void AddPwnedPasswordClient_WithInvalidServiceName_Throws(string serviceName)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPwnedPasswordClient(serviceName);

            var _ = serviceCollection.BuildServiceProvider();
        }


        [TestMethod]
        public void AddBreachClient_AddsBreachClient()
        { 
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBreachClient(new ApiKey("a"), "a");
            var service = serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IBreachClient));

            Assert.IsNotNull(service);
            Assert.AreEqual(ServiceLifetime.Transient, service.Lifetime);
        }

        [TestMethod]
        public void AddBreachClient_AddsPwnedPasswordsClient()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPwnedPasswordClient("a");
            var service = serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IPwnedPasswordClient));

            Assert.IsNotNull(service);
            Assert.AreEqual(ServiceLifetime.Transient, service.Lifetime);
        }

        [TestMethod]
        public void AddBreachClient_AddsPastesClient()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPastesClient(new ApiKey("a"), "a");
            var service = serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IPastesClient));

            Assert.IsNotNull(service);
            Assert.AreEqual(ServiceLifetime.Transient, service.Lifetime);
        }

    }
}
