using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación uno a muchos: usuario tiene muchos teléfonos
            modelBuilder.Entity<Phone>()
                .HasOne(p => p.User)
                .WithMany(u => u.Phones)
                .HasForeignKey(p => p.UserId);

            // Email único
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Phone>().ToTable("Phones");
        }
    }
}
