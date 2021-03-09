namespace Курсач_1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kur3.roles")]
    public partial class roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public roles()
        {
            users = new HashSet<users>();
        }

        [Key]
        public int idrole { get; set; }

        [Required]
        [StringLength(45)]
        public string name { get; set; }

        public int priority { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<users> users { get; set; }

        public roles(string name, int prior)
        {
            this.name = name;
            priority = prior;
        }

        public string Role => name;

        public string Priority => priority.ToString();
    }
}
