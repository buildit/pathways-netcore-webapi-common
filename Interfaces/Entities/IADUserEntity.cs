namespace pathways_common.Interfaces
{
    public interface IADUserEntity : IIdEntity
    {
        string Username { get; set; }
        
        string DirectoryName { get; set; }
    }
}