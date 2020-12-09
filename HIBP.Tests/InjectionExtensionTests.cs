
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
        [TestMethod]
        [DataRow(typeof(IPwnedPasswordClient))]
        [DataRow(typeof(IBreachClient))]
        [DataRow(typeof(IPastesClient))]
        public void AddHIBP_AddsAllClient(Type type)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHIBP(f => { });
            var service = serviceCollection.FirstOrDefault(s => s.ServiceType == type);

            Assert.IsNotNull(service);
            Assert.IsTrue(service.Lifetime == ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void AddBreachClient_AddsBreachClient()
        { 
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBreachClient(new ApiKey("a"), "a");
            var service = serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IBreachClient));

            Assert.IsNotNull(service);
            Assert.IsTrue(service.Lifetime == ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void AddBreachClient_AddsPwnedPasswordsClient()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPwnedPasswordClient("a");
            var service = serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IPwnedPasswordClient));

            Assert.IsNotNull(service);
            Assert.IsTrue(service.Lifetime == ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void AddBreachClient_AddsPastesClient()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPastesClient(new ApiKey("a"), "a");
            var service = serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(IPastesClient));

            Assert.IsNotNull(service);
            Assert.IsTrue(service.Lifetime == ServiceLifetime.Singleton);
        }

    }
}
