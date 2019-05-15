namespace pathways_common.Controllers
{
    using System;
    using Interfaces.Entities;
    using Interfaces.Services;
    using Microsoft.Extensions.Caching.Memory;

    public abstract class CacheResolvingController<T> : ApiController
        where T : INamedEntity
    {
        private readonly IMemoryCache memoryCache;
        private readonly IGetByNameService<T> cachedService;

        protected CacheResolvingController(IGetByNameService<T> cachedService, IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.cachedService = cachedService;
        }

        protected int GetUserId(string identityName)
        {
            return this.memoryCache.GetOrCreate(identityName, e =>
            {
                e.SlidingExpiration = TimeSpan.FromHours(4);
                return this.cachedService.Retrieve(identityName).Id;
            });
        }
    }
}