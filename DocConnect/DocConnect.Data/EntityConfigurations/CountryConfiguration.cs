using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DocConnect.Data.Models.Domains;

namespace DocConnect.Data.EntityConfigurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        private ModelBuilder _modelBuilder;

        public CountryConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            _modelBuilder.Entity<Country>().HasQueryFilter(s => s.IsDeleted == false);

            builder.ToTable("countries");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                .HasColumnName("country_id");

            builder.Property(e => e.CountryName)
                .HasColumnName("country_name");

            builder.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");

            builder.Property(e => e.Iso2Name)
                .HasColumnName("iso2_name");

            builder.Property(e => e.Iso3Name)
                .HasColumnName("iso3_name");
        }
    }
}
