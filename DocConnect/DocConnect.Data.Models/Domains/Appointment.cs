using System.ComponentModel.DataAnnotations.Schema;

namespace DocConnect.Data.Models.Domains
{

    public class Appointment
    {
        /// <summary>
        /// Identificator
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The doctor that the appointment is made for
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Navigational property for the doctor relationship
        /// </summary>
        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; }

        /// <summary>
        /// The patient that made the appointment
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Navigational property for the patient relationship
        /// </summary>
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

        /// <summary>
        /// The time scheduled for the appointment
        /// </summary>
        public DateTime ScheduledTime { get; set; }

        /// <summary>
        /// Notes left by the patient
        /// </summary>
        public string? AppointmentNotes { get; set; }

        /// <summary>
        /// Whether the appointment is canceled
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IsCanceled { get; set; }

        /// <summary>
        /// Whether the recod is deleted or not
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IsDeleted { get; set; }
    }
}
