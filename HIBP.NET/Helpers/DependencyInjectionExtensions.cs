namespace HIBP.Helpers
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Adds the HIBP to the dependency container.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configurationFactory">The configuration factory.</param>
        public static void AddHIBP(this IServiceCollection services, Action<HIBPConfiguration> configurationFactory)
        {
            var config = new HIBPConfiguration();
            configurationFactory(config);

            AddBreachClient(services, config.ApiKey, config.ServiceName);
            AddPwnedPasswordClient(services, config.ServiceName);
            AddPastesClient(services, config.ApiKey, config.ServiceName);
        }

        /// <summary>
        /// Adds the breach client.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <param name="serviceName">Name of the service.</param>
        public static void AddBreachApi(this IServiceCollection services, ApiKey key, string serviceName)
        {
            services.AddSingleton<IBreachClient, BreachClient>(_ => new BreachClient(key, serviceName));
        }

        /// <summary>
        /// Adds the Pwned password client.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="serviceName">Name of the service.</param>
        public static void AddPwnedPasswordApi(this IServiceCollection services, string serviceName)
        {
            services.AddSingleton<IPwnedPasswordClient, PwnedPasswordClient>(_ => new PwnedPasswordClient(serviceName));
        }
        
        /// <summary>
        /// Adds the Pastes client.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <param name="serviceName">Name of the service.</param>
        public static void AddBreachApi(this IServiceCollection services, ApiKey key, string serviceName)
        {
            services.AddSingleton<IPasteClient, PasteClient>(_ => new PasteClient(key, serviceName));
        }
    }
}
