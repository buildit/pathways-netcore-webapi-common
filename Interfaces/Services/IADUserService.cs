namespace pathways_common.Interfaces.Services
{
    public interface IADUserService<T> : ICrudService<T>, IGetByNameService<T>
    {
        T RetrieveOrCreate(string adEmail, string adName);
    }
}