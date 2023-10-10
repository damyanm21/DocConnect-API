using DocConnect.Business.Models.AppointmentDTOs;
using DocConnect.Business.Models.Helpers.ResponseResult;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Contains the business logic related to the appointments
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// Async method to retrieve all patient's upcoming appointments with given criterias.
        /// </summary>
        /// <param name="criterias">Criterias used for filtering the appointments.</param>
        /// <param name="appointmentsType">Upcoming or Past appointments.</param>
        /// <returns>Collection of AppointmentDTO.</returns>
        Task<ICollection<AppointmentDTO>> FilteredPatientAppointmentsAsync(AppointmentInfoCriteriaDTO criterias, string appointmentsType);

        /// <summary>
        /// Async method to get all patient's upcoming appointments.
        /// </summary>
        /// <param name="userId">Patient/User Identificator.</param>
        /// <param name="patientLocalDate">Patient's local date.</param>
        /// <param name="appointmentsType">Upcoming or Past appointments.</param>
        /// <returns>Collection of AppointmentDTO.</returns>
        Task<ICollection<AppointmentDTO>> AllPatientAppointmentsAsync(string userId, string patientLocalDate, string appointmentsType);

        /// <summary>
        /// Async method to schedule an appointment
        /// </summary>
        /// <param name="appointmentDTO">A dto containing the doctors Id, the patients Id and the date and time of the appointment</param>
        /// <returns>A ResponseModel containing information whether the operation was successful or not</returns>
        Task<ResponseModel> ScheduleAnAppointmentAsync(AppointmentScheduleInfoDTO appointmentDTO);

        /// <summary>
        /// Async method to get all doctor's taken appointments
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns>Returns the taken hours as DateTime</returns>
        Task<ICollection<DateTime>> GetTakenAppointmentHoursForDoctorAsync(int doctorId);
    }
}
