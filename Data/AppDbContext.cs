using agendamento_recursos.Models;
using Microsoft.EntityFrameworkCore;

namespace agendamento_recursos.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
