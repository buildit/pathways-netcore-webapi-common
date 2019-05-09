namespace pathways_common.Entities
{
    using Interfaces;

    public abstract class IdEntity : IIdEntity
    {
        protected IdEntity()
        {
        }

        protected IdEntity(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}