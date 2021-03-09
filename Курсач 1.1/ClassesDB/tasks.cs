namespace Курсач_1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    [Table("kur3.tasks")]
    public partial class tasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tasks()
        {
            users_tasks = new HashSet<users_tasks>();
        }

        [Key]
        public int idtask { get; set; }

        [Required]
        [StringLength(40)]
        public string header { get; set; }

        [Required]
        [StringLength(2000)]
        public string text
        {
            get { return Text; }
            set
            {
                Text = value;
                OnPropertyChanged("Text");
            }
        }

        public int? importance { get; set; }

        public int? status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? deadline { get; set; }

        public virtual importance importance1 { get; set; }

        public virtual status status1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<users_tasks> users_tasks { get; set; }


        public tasks(string header, string text, DateTime? date, int hier)
        {
            this.header = header;
            this.Text = text;
            this.deadline = date;
            this.importance = hier;
            this.status = 0;
        }

        public string Text;

        public string Status => status1.statustext;

        public string deadlineDate => deadline.Value.ToString("d");

        public event PropertyChangedEventHandler PropertyChanged1;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged1?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
