using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DocConnect.Data.Models.Domains;

namespace DocConnect.Data.EntityConfigurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        private ModelBuilder _modelBuilder;

        public PatientConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            _modelBuilder.Entity<Patient>().HasQueryFilter(s => s.IsDeleted == false);

            builder.ToTable("patients");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                  .HasColumnName("patient_id");

            builder.Property(e => e.IsDeleted)
                   .HasColumnName("is_deleted");

            builder.Property(e => e.UserId)
                   .HasColumnName("user_id");
        }
    }
}