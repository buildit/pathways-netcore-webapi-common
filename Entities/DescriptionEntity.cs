namespace pathways_common.Entities
{
    using Interfaces.Entities;

    public abstract class DescriptionEntity : NamedEntity, IDescriptionEntity
    {
        public string Description { get; set; }
    }
}