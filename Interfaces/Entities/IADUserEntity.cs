namespace pathways_common.Interfaces.Entities
{
    public interface IADUserEntity : IIdEntity
    {
        string Username { get; set; }

        string DirectoryName { get; set; }
    }
}