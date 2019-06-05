namespace pathways_common.Controllers
{
    using Authentication;
    using Authentication.TokenAcquisition;
    using Graph;
    using Microsoft.Graph;

    public abstract class GraphedApiController : ApiController
    {
        private readonly ITokenAcquisition tokenAcquisition;

        protected GraphedApiController(ITokenAcquisition tokenAcquisition)
        {
            this.tokenAcquisition = tokenAcquisition;
        }

        protected GraphServiceClient GetGraphServiceClient(string[] scopes)
        {
            return GraphServiceClientFactory.GetAuthenticatedGraphClient(async () =>
            {
                string result = await this.tokenAcquisition.GetAccessTokenOnBehalfOfUser(this.HttpContext, scopes);
                return result;
            }, GraphConstants.Url);
        }
    }
}