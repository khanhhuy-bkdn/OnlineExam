namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OnlineExamDbContext : DbContext
    {
        public OnlineExamDbContext()
            : base("name=OnlineExamDbContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Lession> Lessions { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.AnswerA)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.AnswerB)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.AnswerC)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.AnswerD)
                .IsUnicode(false);
        }
    }
}
