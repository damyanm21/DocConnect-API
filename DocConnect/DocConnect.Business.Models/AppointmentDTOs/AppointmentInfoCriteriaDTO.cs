namespace DocConnect.Business.Models.AppointmentDTOs
{
    /// <summary>
    /// DTO used for filtering an Upcoming and Past Appointments.
    /// </summary>
    public class AppointmentInfoCriteriaDTO
    {
        public string UserId { get; set; }

        public string? SpecialistName { get; set; }

        public string? SpecialtyName { get; set; }

        public string? From { get; set; }

        public string? To { get; set; }

        public string? PatientLocalDate { get; set; }
    }
}

