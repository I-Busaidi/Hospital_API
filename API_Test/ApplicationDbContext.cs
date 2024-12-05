using API_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Test
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clinic>()
                .HasIndex(c => c.cSpec)
                .IsUnique();
        }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> appointments { get; set; }
    }
}
