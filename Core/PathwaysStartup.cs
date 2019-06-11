namespace pathways_common.Core
{
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Authentication;
    using Authentication.Extensions;
    using Authentication.TokenAcquisition;
    using Authentication.TokenCache;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.AzureAD.UI;
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
            services.AddOptions();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            this.SetupAdAuthenticationV2(services);
            this.AddEntityFramework(services);

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

        private void SetupAdAuthenticationV2(IServiceCollection services)
        {
            services.AddAuthentication(AzureADDefaults.JwtBearerAuthenticationScheme)
                .AddAzureADBearer(options => this.Configuration.Bind("AzureAd", options));

            services.AddSession();
            services.AddTokenAcquisition();
            // Added
            services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
            {
                var scopes = new[] { PathwaysConstants.Graph.ScopeUserRead };
                this.Configuration.Bind("AzureAd", options);
                // This is an Azure AD v2.0 Web API
                options.Authority += "/v2.0";

                // The valid audiences are both the Client ID (options.Audience) and api://{ClientID}
                options.TokenValidationParameters.ValidAudiences = new[] { options.Audience, $"api://{options.Audience}" };

                // Instead of using the default validation (validating against a single tenant, as we do in line of business apps),
                // we inject our own multitenant validation logic (which even accepts both V1 and V2 tokens)
                options.TokenValidationParameters.IssuerValidator = AadIssuerValidator.ForAadInstance(options.Authority).ValidateAadIssuer;

                // When an access token for our own Web API is validated, we add it to MSAL.NET's cache so that it can
                // be used from the controllers.
                options.Events = new JwtBearerEvents();

                // If you want to debug, or just understand the JwtBearer events, uncomment the following line of code
                options.Events = JwtBearerMiddlewareDiagnostics.Subscribe(options.Events);

                options.Events.OnTokenValidated = async context =>
                {
                    if (scopes != null && scopes.Any())
                    {
                        var tokenAcquisition = context.HttpContext.RequestServices.GetRequiredService<ITokenAcquisition>();
                        context.Success();
                        tokenAcquisition.AddAccountToCacheFromJwt(context, scopes);
                    }
                    else
                    {
                        context.Success();

                        // Todo : rather use options.SaveToken?
                        (context.Principal.Identity as ClaimsIdentity).AddClaim(new Claim("jwt", (context.SecurityToken as JwtSecurityToken).RawData));
                    }

                    // Adds the token to the cache, and also handles the incremental consent and claim challenges
                    await Task.FromResult(0);
                };
            });
            
            services.AddMsal(new[] { PathwaysConstants.Graph.ScopeUserRead });
            services.AddInMemoryTokenCaches();
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
                        ValidIssuers = new List<string> { $"https://sts.windows.net/{this.Configuration["AzureAd:TenantId"]}/", $"{this.Configuration["AzureAd:Instance"]}{this.Configuration["AzureAd:TenantId"]}/v2.0" }
                    };
                });
        }
    }
}