using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DocConnect.Data.Models.Domains;

namespace DocConnect.Data.EntityConfigurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        private ModelBuilder _modelBuilder;

        public LocationConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Configure(EntityTypeBuilder<Location> builder)
        {
            _modelBuilder.Entity<Location>().HasQueryFilter(s => s.IsDeleted == false);

            builder.ToTable("locations");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                   .HasColumnName("location_id");

            builder.Property(e => e.CityAscii)
                .HasColumnName("city_ascii");

            builder.Property(e => e.CityName)
                .HasColumnName("city_name");

            builder.Property(e => e.CountryId)
                .HasColumnName("country_id");

            builder.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");

            builder.Property(e => e.Latitude)
                .HasColumnName("latitude");

            builder.Property(e => e.Longitude)
                .HasColumnName("longitude");
        }
    }
}
