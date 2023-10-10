using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DocConnect.Data.Models.Domains;

namespace DocConnect.Data.EntityConfigurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        private ModelBuilder _modelBuilder;

        public DoctorConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            _modelBuilder.Entity<Doctor>().HasQueryFilter(s => s.IsDeleted == false);

            builder.ToTable("doctors");

            builder.HasKey(e => e.Id)
                   .HasName("PRIMARY");

            builder.Property(e => e.Id)
                   .HasColumnName("doctor_id");

            builder.Property(e => e.DoctorSummary)
                   .HasColumnName("doctor_summary");

            builder.Property(e => e.FirstName)
                   .HasColumnName("doctor_first_name");

            builder.Property(e => e.LastName)
                   .HasColumnName("doctor_last_name");

            builder.Property(e => e.ImageUrl)
                  .HasColumnName("image_url");

            builder.Property(e => e.EducationSummary)
                   .HasColumnName("education_summary");

            builder.Property(e => e.IsDeleted)
                   .HasColumnName("is_deleted");

            builder.Property(e => e.LocationId)
                   .HasColumnName("location_id");

            builder.Property(e => e.SpecialtyId)
                   .HasColumnName("specialty_id");

            builder.Property(e => e.UserId)
                   .HasColumnName("user_id");

            builder.Property(e => e.YearsOfExperience)
                   .HasColumnName("years_of_experience");

            builder.Property(e => e.Address)
                   .HasColumnName("doctor_address");
        }
    }
}
