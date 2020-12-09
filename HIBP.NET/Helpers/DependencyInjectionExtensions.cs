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

            if (config.ApiKey == null)
            {
                throw new InvalidApiKeyException();
            }

            if (string.IsNullOrWhiteSpace(config.ServiceName))
            {
                throw new InvalidServiceNameException();
            }

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
        public static void AddBreachClient(this IServiceCollection services, ApiKey apiKey, string serviceName)
        {
            if (apiKey == null)
            {
                throw new InvalidApiKeyException();
            }

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new InvalidServiceNameException();
            }

            services.AddHttpClient<IBreachClient, BreachClient>(nameof(BreachClient), _ => new BreachClient(apiKey, serviceName));
        }

        /// <summary>
        /// Adds the Pwned password client.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="serviceName">Name of the service.</param>
        public static void AddPwnedPasswordClient(this IServiceCollection services, string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new InvalidServiceNameException();
            }

            services.AddHttpClient<IPwnedPasswordClient, PwnedPasswordClient>(nameof(PwnedPasswordClient), _ => new PwnedPasswordClient(serviceName));
        }

        /// <summary>
        /// Adds the Pastes client.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <param name="serviceName">Name of the service.</param>
        public static void AddPastesClient(this IServiceCollection services, ApiKey apiKey, string serviceName)
        {
            if (apiKey == null)
            {
                throw new InvalidApiKeyException();
            }

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new InvalidServiceNameException();
            }

            services.AddHttpClient<IPastesClient, PastesClient>(nameof(PastesClient), _ => new PastesClient(apiKey, serviceName));
        }
    }
}
