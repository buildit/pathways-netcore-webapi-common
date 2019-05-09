namespace pathways_common.Interfaces
{
    public interface IResolveService<out T>
    {
        T Retrieve(string lookupValue);
    }
}