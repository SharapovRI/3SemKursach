namespace Курсач_1._1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<changedtasks> changedtasks { get; set; }
        public virtual DbSet<importance> importance { get; set; }
        public virtual DbSet<roles> roles { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<tasks> tasks { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<users_tasks> users_tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<changedtasks>()
                .HasOptional(e => e.users)
                .WithRequired(e => e.changedtasks)
                .WillCascadeOnDelete();

            modelBuilder.Entity<importance>()
                .Property(e => e.importancetext)
                .IsUnicode(false);

            modelBuilder.Entity<importance>()
                .HasMany(e => e.tasks)
                .WithOptional(e => e.importance1)
                .HasForeignKey(e => e.importance);

            modelBuilder.Entity<roles>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<roles>()
                .HasMany(e => e.users)
                .WithOptional(e => e.roles)
                .HasForeignKey(e => e.role);

            modelBuilder.Entity<status>()
                .Property(e => e.statustext)
                .IsUnicode(false);

            modelBuilder.Entity<status>()
                .HasMany(e => e.tasks)
                .WithOptional(e => e.status1)
                .HasForeignKey(e => e.status);

            modelBuilder.Entity<tasks>()
                .Property(e => e.header)
                .IsUnicode(false);

            modelBuilder.Entity<tasks>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<tasks>()
                .HasMany(e => e.users_tasks)
                .WithRequired(e => e.tasks)
                .HasForeignKey(e => e.idtasks);

            modelBuilder.Entity<users>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.users_tasks)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.idusers);
        }
    }
}
