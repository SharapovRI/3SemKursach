namespace Курсач_1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kur3.users_tasks")]
    public partial class users_tasks
    {
        [Key]
        public int idusers_tasks { get; set; }

        public int idusers { get; set; }

        public int idtasks { get; set; }

        public virtual tasks tasks { get; set; }

        public virtual users users { get; set; }

        public users_tasks(int text)
        {
            this.idusers = text;
        }

        public users_tasks()
        { 
        }
    }
}
