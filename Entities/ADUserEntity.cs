namespace pathways_common.Entities
{
    using Interfaces.Entities;

    public abstract class ADUserEntity : NamedEntity, IADUserEntity
    {
        public string Username { get; set; }

        public string DirectoryName { get; set; }
    }
}