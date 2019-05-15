namespace pathways_common.Interfaces.Entities
{
    public interface INamedEntity : IIdEntity
    {
        string Name { get; set; }
    }
}