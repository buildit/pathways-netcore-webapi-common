namespace pathways_common.Services
{
    using System.Threading.Tasks;
    using Authentication;
    using Authentication.TokenAcquisition;
    using Core;
    using Interfaces.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Graph;

    public class MicrosoftGraphService : IMSGraphService
    {
        private readonly ITokenAcquisition tokenAcquisition;

        public MicrosoftGraphService(ITokenAcquisition tokenAcquisition)
        {
            this.tokenAcquisition = tokenAcquisition;
        }

        public IGraphServiceClient GetGraphServiceClient(HttpContext context, string[] scopes)
        {
            Task<string> TokenProvider()
            {
                return this.tokenAcquisition.GetAccessTokenOnBehalfOfUser(context, scopes);
            }

            return new GraphServiceClient(PathwaysConstants.Graph.Url, new CustomAuthenticationProvider(TokenProvider));
        }
    }
}