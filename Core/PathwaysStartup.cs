namespace pathways_common.Core
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class PathwaysStartup
    {
        protected PathwaysStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        protected abstract void AddEntityFramework(IServiceCollection services);

        protected void ConfigureStandardStack(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            this.AddEntityFramework(services);
            this.AddAdditionalServices(services);
            services.AddMemoryCache();
        }

        protected abstract void AddAdditionalServices(IServiceCollection services);
    }
}