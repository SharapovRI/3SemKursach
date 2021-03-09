namespace Курсач_1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;

    [Table("kur3.users")]
    public partial class users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public users()
        {
            users_tasks = new HashSet<users_tasks>();
        }

        [Key]
        public int iduser { get; set; }

        [Required]
        [StringLength(45)]
        public string login
        {
            get { return Login; }
            set
            {
                Login = value;
                OnPropertyChanged("Login");
            }
        }

        [Required]
        [StringLength(128)]
        public string password { get; set; }

        public int? role { get; set; }

        public virtual changedtasks changedtasks { get; set; }

        public virtual roles roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<users_tasks> users_tasks { get; set; }


        public users(string login, string password, int role)
        {
            this.login = login;
            this.password = password;
            this.role = role;
        }

        public string Login;

        public event PropertyChangedEventHandler PropertyChanged1;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged1?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
