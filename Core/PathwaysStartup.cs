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
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;

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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

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
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = $"{this.Configuration["AzureAd:ClientId"]}",
                        //ValidIssuer = $"https://sts.windows.net/{azureadoptions.TenantId}/" // for "signInAudience": "AzureADMyOrg" or "AzureADMultipleOrgs"
                        // ValidIssuer = $"{azureadoptions.Instance}{azureadoptions.TenantId}/v2.0" // for "signInAudience": "AzureADandPersonalMicrosoftAccount"
                        ValidIssuers = new List<string> { $"https://sts.windows.net/{this.Configuration["AzureAd:TenantId"]}/", $"{this.Configuration["AzureAd:Instance"]}{this.Configuration["AzureAd:TenantId"]}/v2.0" }
                    };
                    // options.TokenValidationParameters //1a6dbb80-5290-4fd1-a938-0ad7795dfd7a/v2.0'
                });
        }
    }
}