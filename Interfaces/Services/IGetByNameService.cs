namespace pathways_common.Interfaces
{
    public interface IGetByNameService<out T>
    {
        T Retrieve(string name);
    }
}