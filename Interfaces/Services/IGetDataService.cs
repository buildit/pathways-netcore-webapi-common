namespace pathways_common.Interfaces
{
    using System.Collections.Generic;

    public interface IGetDataService<out T>
    {
        T Retrieve(int id);

        IEnumerable<T> GetAll();
    }
}