namespace pathways_common.Controllers
{
    using System;
    using Interfaces;
    using Microsoft.Extensions.Caching.Memory;

    public abstract class CacheResolvingController<T> : ApiController
        where T : INamedEntity
    {
        private readonly IMemoryCache memoryCache;
        private readonly IResolveService<T> userService;

        protected CacheResolvingController(IResolveService<T> cacheService, IMemoryCache memoryCache)
        {
            this.userService = cacheService;
            this.memoryCache = memoryCache;
        }

        protected int GetUserId(string identityName)
        {
            return this.memoryCache.GetOrCreate(identityName, e =>
            {
                e.SlidingExpiration = TimeSpan.FromHours(4);
                return this.userService.Retrieve(identityName).Id;
            });
        }
    }
}