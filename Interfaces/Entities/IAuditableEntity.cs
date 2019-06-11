namespace pathways_common.Interfaces.Entities
{
    using System;

    public interface IAuditableEntity : IIdEntity
    {
        DateTime CreatedDate { get; set; }

        DateTime ModifiedDate { get; set; }
    }
}