namespace pathways_common.Interfaces.Services
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Graph;

    public interface IMSGraphService
    {
        IGraphServiceClient GetGraphServiceClient(HttpContext context, string[] scopes);
    }
}