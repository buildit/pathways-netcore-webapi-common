namespace pathways_common.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("exceptions", Schema = "admin")]
    public class ExceptionEntity : IdEntity
    {
        public ExceptionEntity()
            : this(string.Empty)
        {
        }

        public ExceptionEntity(Exception ex, string user = null)
            : this(ex.ToString(), user)
        {
        }

        public ExceptionEntity(string message, string user = null)
        {
            this.Message = message;
            this.User = user;
            this.Time = DateTime.Now;
        }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        public string User { get; set; }
    }
}