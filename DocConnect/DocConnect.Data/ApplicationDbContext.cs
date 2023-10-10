using DocConnect.Data.EntityConfigurations;
using DocConnect.Data.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SpecialtyConfiguration(modelBuilder));
            modelBuilder.ApplyConfiguration(new CountryConfiguration(modelBuilder));
            modelBuilder.ApplyConfiguration(new DoctorConfiguration(modelBuilder));
            modelBuilder.ApplyConfiguration(new LocationConfiguration(modelBuilder));
            modelBuilder.ApplyConfiguration(new PatientConfiguration(modelBuilder));
            modelBuilder.ApplyConfiguration(new RatingConfiguration(modelBuilder));
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration(modelBuilder));

            base.OnModelCreating(modelBuilder);

        }
    }
}

