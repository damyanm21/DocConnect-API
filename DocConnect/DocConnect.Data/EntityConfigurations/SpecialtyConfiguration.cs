using DocConnect.Data.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    /// <summary>
    /// Class used to configure the Specialty Entity.
    /// </summary>
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        private ModelBuilder _modelBuilder;

        public SpecialtyConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            _modelBuilder.Entity<Specialty>().HasQueryFilter(s => s.IsDeleted == false);

            builder.ToTable("specialties");

            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .HasColumnName("specialty_id");

            builder
               .Property(e => e.ImageUrl)
               .HasColumnName("image_url");

            builder
               .Property(e => e.Name)
               .HasColumnName("specialty_name");

            builder
              .Property(e => e.IsDeleted)
              .HasColumnName("is_deleted");
        }
    }
}
