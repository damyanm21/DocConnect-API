using DocConnect.Data.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        private ModelBuilder _modelBuilder;

        public AppointmentConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            _modelBuilder.Entity<Appointment>().HasQueryFilter(s => s.IsDeleted == false);

            builder.ToTable("appointments");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                .HasColumnName("appointment_id");

            builder.Property(e => e.DoctorId)
                .HasColumnName("doctor_id");

            builder.Property(e => e.PatientId)
                .HasColumnName("patient_id");

            builder.Property(e => e.ScheduledTime)
                .HasColumnName("scheduled_time");

            builder.Property(e => e.AppointmentNotes)
                .HasColumnName("appointment_notes");

            builder.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");

            builder.Property(e => e.IsCanceled)
                .HasColumnName("is_canceled");
        }

    }
}
