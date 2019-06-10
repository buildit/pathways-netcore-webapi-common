namespace pathways_common.Interfaces.Services
{
    public interface IADUserService<T> : ICrudService<T>, IGetByNameService<T>
    {
        void SetLogonTime(T user);

        T RetrieveOrCreate(string graphEmail, string adEmail, string adName);
    }
}