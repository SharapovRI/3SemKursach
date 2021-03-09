namespace Курсач_1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kur3.changedtasks")]
    public partial class changedtasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idchangedtasks { get; set; }

        public int addedtasks { get; set; }

        [Column("changedtasks")]
        public int changedtasks1 { get; set; }

        public int deletedtasks { get; set; }

        public int misseddeadline { get; set; }

        public virtual users users { get; set; }

        public changedtasks(users user)
        {
            this.idchangedtasks = user.iduser;
        }

        public changedtasks()
        {

        }
    }
}
