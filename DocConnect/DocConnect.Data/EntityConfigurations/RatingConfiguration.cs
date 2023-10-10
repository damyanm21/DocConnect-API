using DocConnect.Data.Models.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.EntityConfigurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        private ModelBuilder _modelBuilder;

        public RatingConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            _modelBuilder.Entity<Rating>().HasQueryFilter(s => s.IsDeleted == false);

            builder.ToTable("ratings");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                   .HasColumnName("rating_id");

            builder.Property(e => e.Comments)
                .HasColumnName("comments");

            builder.Property(e => e.DoctorId)
                .HasColumnName("doctor_id");

            builder.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");

            builder.Property(e => e.PatientId)
                .HasColumnName("patient_id");

            builder.Property(e => e.RatingPoints)
                .HasColumnName("rating");
        }
    }
}
