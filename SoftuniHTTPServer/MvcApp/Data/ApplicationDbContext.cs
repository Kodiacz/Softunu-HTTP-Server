namespace MvcApp.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=LAPTOP-6HEN1T16\SQLEXPRESS;Database=GitProject;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commit>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.Commits)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder); 
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Repository> Repositories { get; set; }

        public DbSet<Commit> Commits { get; set; }
    }
}
