namespace pathways_common.Entities
{
    using System;

    public abstract class PathwaysUser : ADUserEntity
    {
        protected PathwaysUser()
        {
        }

        protected PathwaysUser(string username, string directoryName)
        {
            this.Username = username;
            this.DirectoryName = directoryName;
        }

        public DateTime LastLogin { get; set; }

        public string OrganizationId { get; set; }
    }
}