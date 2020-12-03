namespace HIBP
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

            AddBreachApi(services, config.ApiKey, config.ServiceName);
            AddPwnedPasswordApi(services, config.ServiceName);
        }

        /// <summary>
        /// Adds the breach API.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <param name="serviceName">Name of the service.</param>
        public static void AddBreachApi(this IServiceCollection services, ApiKey key, string serviceName)
        {
            services.AddSingleton<IBreachApi, BreachApi>(_ => new BreachApi(key, serviceName));
        }

        /// <summary>
        /// Adds the Pwned password API.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="serviceName">Name of the service.</param>
        public static void AddPwnedPasswordApi(this IServiceCollection services, string serviceName)
        {
            services.AddSingleton<IPwnedPasswordApi, PwnedPasswordApi>(_ => new PwnedPasswordApi(serviceName));
        }
    }
}
