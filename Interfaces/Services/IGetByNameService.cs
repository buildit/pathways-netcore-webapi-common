namespace pathways_common.Interfaces.Services
{
    public interface IGetByNameService<out T>
    {
        T Retrieve(string name);
    }
}