namespace pathways_common.Authentication
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.Graph;

    class CustomAuthenticationProvider : IAuthenticationProvider
    {
        public CustomAuthenticationProvider(Func<Task<string>> acquireTokenCallback)
        {
            this.acquireAccessToken = acquireTokenCallback;
        }

        private readonly Func<Task<string>> acquireAccessToken;

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            string accessToken = await this.acquireAccessToken.Invoke();

            request.Headers.Authorization = new AuthenticationHeaderValue(PathwaysConstants.Graph.BearerAuthorizationScheme, accessToken);
        }
    }
}