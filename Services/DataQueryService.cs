namespace pathways_common.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Entities;
    using Interfaces.Services;
    using Microsoft.EntityFrameworkCore;

    public abstract class DataQueryService<T, T2> : IGetDataService<T>
        where T : class, IIdEntity where T2 : DbContext
    {
        protected readonly IEnumerable<T> collection;
        protected readonly T2 context;

        protected DataQueryService(T2 context, IEnumerable<T> collection)
        {
            this.context = context;
            this.collection = collection;
        }

        public T Retrieve(int id)
        {
            return this.context.Query<T>().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.context.Set<T>();
        }
    }
}