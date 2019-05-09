namespace pathways_common.Entities
{
    using Interfaces;

    public abstract class NamedEntity : IdEntity, INamedEntity
    {
        protected NamedEntity()
        {
        }

        protected NamedEntity(int id, string name) 
            : base(id)
        {
            this.Name = name;
        }

        public virtual string Name { get; set; }
    }
}