namespace DocConnect.Business.Models.AppointmentDTOs
{
    /// <summary>
    /// DTO used for upcoming and past appointment's.
    /// </summary>
    public class AppointmentDTO
    {
        public int Id { get; set; }

        public string DoctorName { get; set; }

        public string PatientName { get; set; }

        public string DoctorSpecialty { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Address { get; set; }
    }
}

