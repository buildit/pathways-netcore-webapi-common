namespace pathways_common.Entities
{
    using System;

    public abstract class AuditedNamedEntity : NamedEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}