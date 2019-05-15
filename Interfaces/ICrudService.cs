namespace pathways_common.Interfaces
{
    public interface ICrudService<T> : IGetDataService<T>
    {
        T Create(T user);

        void Update(int userId, T user);

        void Delete(int userId, int id);

        T GetByIdWithIncludes(int id);
    }
}