using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TokenCache.Tests.Core")]

namespace pathways_common.Extensions
{
    using Authentication.TokenAcquisition;
    using Authentication.TokenCache;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods
    /// </summary>
    public static class TokenAcquisitionExtension
    {
        /// <summary>
        /// Add the token acquisition service.
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>the service collection</returns>
        /// <example>
        /// This method is typically called from the Startup.ConfigureServices(IServiceCollection services)
        /// Note that the implementation of the token cache can be chosen separately.
        ///
        /// <code>
        /// // Token acquisition service and its cache implementation as a session cache
        /// services.AddTokenAcquisition()
        /// .AddDistributedMemoryCache()
        /// .AddSession()
        /// .AddSessionBasedTokenCache()
        ///  ;
        /// </code>
        /// </example>
        public static IServiceCollection AddTokenAcquisition(this IServiceCollection services)
        {
            // Token acquisition service
            services.AddScoped<ITokenAcquisition>(factory =>
            {
                var config = factory.GetRequiredService<IConfiguration>();
                var apptokencacheprovider = factory.GetService<IMSALAppTokenCacheProvider>();
                var usertokencacheprovider = factory.GetService<IMSALUserTokenCacheProvider>();

                return new TokenAcquisition(config, apptokencacheprovider, usertokencacheprovider);
            });

            return services;
        }
    }
}