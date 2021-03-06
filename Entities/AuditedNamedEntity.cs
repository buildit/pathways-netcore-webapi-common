namespace pathways_common.Entities
{
    using System;
    using Interfaces.Entities;

    public abstract class AuditedNamedEntity : NamedEntity, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}