namespace pathways_common.Interfaces
{
    public interface ICrudService<T> : IGetDataService<T>
    {
        T Create(T entity);

        void Update(T entity);

        void Delete(int id);

        T GetByIdWithIncludes(int id);
    }
}