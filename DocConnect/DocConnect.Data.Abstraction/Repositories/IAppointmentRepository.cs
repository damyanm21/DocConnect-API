using System.Linq.Expressions;
using DocConnect.Data.Models.Domains;

namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// A repository interface providing data operations with the appointment entity 
    /// </summary>
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        /// <summary>
        /// Async method to get all patient's appointments based on a given criterias.
        /// </summary>
        /// <param name="expression">Expression used to filter the appointments.</param>
        /// <param name="appointmentsType">Upcoming or Past appointments.</param>
        /// <returns>Collection of AppointmentDTO.</returns>
        Task<ICollection<Appointment>> FilteredAppointmentsAsync(Expression<Func<Appointment, bool>> expression, string appointmentsType);

        /// <summary>
        /// Async method to get all patient's appointments.
        /// </summary>
        /// <param name="expression">Expression used to get the correct appointments.</param>
        /// <param name="appointmentsType">Upcoming or Past appointments.</param>
        /// <returns>Collection of AppointmentDTO.</returns>
        /// 
        Task<ICollection<Appointment>> AllPatientAppointmentsAsync(Expression<Func<Appointment, bool>> expression, string appointmentsType);

        /// <summary>
        /// Checks if the doctor is free for an appointment at that specific day and time
        /// </summary>
        /// <param name="doctorId">The doctor whose appointments to check</param>
        /// <param name="scheduledTime">The day and time of the appointment</param>
        /// <returns>True if the doctor has the spot for the appointment free, false if the spot is already taken</returns>
        Task<bool> IsDoctorAppointmentFreeAsync(int doctorId, DateTime scheduledTime);

        /// <summary>
        /// Checks if the patient has not already scheduled appointment for the same day and time with another doctor
        /// </summary>
        /// <param name="patientId">The patient whose appointments to check</param>
        /// <param name="scheduledTime">The day and time of the appointment</param>
        /// <returns>True if the patient has not scheduled an appointment with another doctor for that time, false if he already has an appointment</returns>
        Task<bool> IsPatientAppointmentFreeAsync(int patientId, DateTime scheduledTime);

        /// <summary>
        /// Async method to get all doctor's taken appointments
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns>Returns the taken hours as DateTime</returns>
        Task<ICollection<DateTime>> GetTakenAppointmentHoursForDoctorAsync(int doctorId);
    }
}
