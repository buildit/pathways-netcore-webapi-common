namespace pathways_common.Core
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Logging;

    public abstract class PathwaysStartup
    {
        protected PathwaysStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        protected abstract void AddEntityFramework(IServiceCollection services);

        protected void ConfigurePathwaysServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            this.AddEntityFramework(services);
            this.SetupAzureAdAuth(services);
            services.AddMemoryCache();
        }

        protected void ConfigurePathways(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseAuthentication();
            app.UseMvc();
        }

        private void SetupAzureAdAuth(IServiceCollection services)
        {
            services
                .AddAuthentication(sharedOptions => { sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
                .AddJwtBearer(options =>
                {
                    options.Audience = this.Configuration["AzureAd:ClientId"];
                    options.Authority = $"{this.Configuration["AzureAd:Instance"]}{this.Configuration["AzureAd:TenantId"]}";
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidAudience = $"{this.Configuration["AzureAd:ClientId"]}",
                        ValidIssuers = new List<string> { $"https://sts.windows.net/{this.Configuration["AzureAd:TenantId"]}/", $"{this.Configuration["AzureAd:Instance"]}{this.Configuration["AzureAd:TenantId"]}/v2.0" }
                    };
                });
        }
    }
}