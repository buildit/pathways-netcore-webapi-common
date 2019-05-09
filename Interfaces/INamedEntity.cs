namespace pathways_common.Interfaces
{
    public interface INamedEntity : IIdEntity
    {
        string Name { get; set; }
    }
}