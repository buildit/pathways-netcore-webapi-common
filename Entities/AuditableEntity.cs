namespace pathways_common.Entities
{
    using System;
    using Interfaces.Entities;

    public abstract class AuditableEntity : IdEntity, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}