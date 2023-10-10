using System.Linq.Expressions;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.AppointmentDTOs;
using DocConnect.Business.Models.Helpers.ResponseResult;
using DocConnect.Business.Models.Structs;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;
using GeoTimeZone;
using TimeZoneConverter;
using static DocConnect.Business.Utilities.ErrorMessages;
using static DocConnect.Data.Models.Utilities.Constants.DoctorConstants;
using static DocConnect.Data.Models.Utilities.Constants.PatientConstants;


namespace DocConnect.Business.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(
            IAppointmentRepository appointmentRepository
            , ISpecialistRepository specialistRepository
            , IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _specialistRepository = specialistRepository;
            _patientRepository = patientRepository;
        }

        public async Task<ResponseModel> ScheduleAnAppointmentAsync(AppointmentScheduleInfoDTO appointmentDTO)
        {
            if (appointmentDTO.Date.Hour < FirstAppointmentHour || appointmentDTO.Date.Hour > LastAppointmentHour)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, AppointmentHourOutOfRange);
            }

            var doctorLocationInfo = await _specialistRepository.GetDoctorsLocationDataAsync(appointmentDTO.DoctorId);

            var doctorTimeZone = TimeZoneLookup.GetTimeZone((double)doctorLocationInfo.Latitude, (double)doctorLocationInfo.Longitude).Result;
            var timeZoneId = TZConvert.IanaToWindows(doctorTimeZone);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            var appointmentDate = new DateTime(
                day: appointmentDTO.Date.Day,
                month: appointmentDTO.Date.Month,
                year: appointmentDTO.Date.Year,
                hour: appointmentDTO.Date.Hour,
                minute: 0,
                second: 0);

            var utcAppointmentTime = TimeZoneInfo.ConvertTimeToUtc(appointmentDate, timeZoneInfo);

            // Check if the appointment time is in the past
            if (utcAppointmentTime < DateTime.UtcNow)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, AppointmentPastDate);
            }


            bool isDoctorFree = await _appointmentRepository.IsDoctorAppointmentFreeAsync(appointmentDTO.DoctorId, utcAppointmentTime);

            if (!isDoctorFree)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, DoctorAppointmentTaken);
            }

            var patientId = await _patientRepository.GetPatientIdByUserIdAsync(appointmentDTO.UserId);
            bool isPatientFree = await _appointmentRepository.IsPatientAppointmentFreeAsync(patientId, utcAppointmentTime);

            if (!isPatientFree)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, PatientAppointmentTaken);
            }

            // Check if the appointment date is one day from tomorrow
            var tomorrow = DateTime.UtcNow.Date.AddDays(2);

            var oneMonthFromTomorrow = tomorrow.AddMonths(1);

            if (utcAppointmentTime > oneMonthFromTomorrow)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, AppointmentMoreThanOneMonth);
            }

            var appointment = new Appointment
            {
                DoctorId = appointmentDTO.DoctorId,
                PatientId = patientId,
                ScheduledTime = utcAppointmentTime
            };

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return HttpResponseHelper.Success(HttpStatusCode.Created);
        }

        public async Task<ICollection<AppointmentDTO>> FilteredPatientAppointmentsAsync(AppointmentInfoCriteriaDTO criterias, string appointmentsType)
        {
            var expression = BuildExpressionForAppointmentsFiltering(criterias, appointmentsType);

            var appointments = await _appointmentRepository.FilteredAppointmentsAsync(expression, appointmentsType);

            var dtos = OrderAppointments(appointments, appointmentsType);

            return dtos;
        }

        public async Task<ICollection<AppointmentDTO>> AllPatientAppointmentsAsync(string userId, string patientLocalDate, string appointmentsType)
        {
            var expression = BuildExpressionForAllAppointments(userId, patientLocalDate, appointmentsType);

            var appointments = await _appointmentRepository.AllPatientAppointmentsAsync(expression, appointmentsType);

            var dtos = OrderAppointments(appointments, appointmentsType);

            return dtos;
        }

        public async Task<ICollection<DateTime>> GetTakenAppointmentHoursForDoctorAsync(int doctorId)
        {
            var takenHoursUtc = await _appointmentRepository.GetTakenAppointmentHoursForDoctorAsync(doctorId);

            if (takenHoursUtc == null || takenHoursUtc.Count == 0)
            {
                return takenHoursUtc;
            }

            var doctorLocationInfo = await _specialistRepository.GetDoctorsLocationDataAsync(doctorId);
            var doctorTimeZone = TimeZoneLookup.GetTimeZone((double)doctorLocationInfo.Latitude, (double)doctorLocationInfo.Longitude).Result;
            var timeZoneId = TZConvert.IanaToWindows(doctorTimeZone);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            var takenHoursLocalTime = takenHoursUtc.Select(utcTime => TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo)).ToList();

            return takenHoursLocalTime;
        }

        /// <summary>
        /// Private method to order the Appointments based on a given appointmentsType.
        /// </summary>
        /// <param name="appointments">Collection with the Appointments.</param>
        /// <param name="appointmentsType">Upcoming or Past appointments type.</param>
        /// <returns>Order Collection with AppointmentDTO</returns>
        private ICollection<AppointmentDTO> OrderAppointments(ICollection<Appointment> appointments, string appointmentsType)
        {
            IOrderedEnumerable<Appointment> orderedAppointments;

            if (appointmentsType == AppointmentsType.Upcoming)
            {
                orderedAppointments = appointments
                  .OrderBy(a => a.ScheduledTime.Date)
                  .ThenBy(a => a.ScheduledTime.Hour);
            }
            else
            {
                orderedAppointments = appointments
                  .OrderByDescending(a => a.ScheduledTime.Date)
                  .ThenBy(a => a.ScheduledTime.Hour);
            }

            return orderedAppointments
                .Select(a => new AppointmentDTO
                {
                    Id = a.Id,
                    PatientName = string.Format(PatientFullName, a.Patient.User.FirstName, a.Patient.User.LastName),
                    DoctorName = string.Format(DoctorFullName, a.Doctor.FirstName, a.Doctor.LastName),
                    DoctorSpecialty = a.Doctor.Specialty.Name,
                    Date = a.ScheduledTime.ToShortDateString(),
                    Time = a.ScheduledTime.ToShortTimeString(),
                    Address = string.Format(FullAddressBuilder, a.Doctor.Address, a.Doctor.Location.CityName)
                })
                .ToList();
        }

        /// <summary>
        /// Private method to build an expression when a Patient wants all his appointments.
        /// </summary>
        /// <param name="userId">Patient Identificator.</param>
        /// <param name="patientLocalDate">Patient's local date.</param>
        /// <param name="appointmentsType">Upcoming or Past.</param>
        /// <returns>Expression based on given appointments type and other criterias.</returns>
        private Expression<Func<Appointment, bool>> BuildExpressionForAllAppointments(string userId, string patientLocalDate, string appointmentsType)
        {
            DateTime from = new DateTime();

            bool isParsed = DateTime.TryParse(patientLocalDate, out from);

            // Expression based on given appointments type and patient local date.
            Expression<Func<Appointment, bool>> expression
                = a => a.Patient.UserId == userId
                && (appointmentsType == AppointmentsType.Upcoming
                   ? (isParsed ? a.ScheduledTime >= from : a.ScheduledTime >= DateTime.UtcNow)
                   : (isParsed ? a.ScheduledTime <= from : a.ScheduledTime <= DateTime.UtcNow));

            return expression;
        }

        /// <summary>
        /// Private method to validate the criterias given by the patient and build an expression for filtering.
        /// </summary>
        /// <param name="criterias">Criterias DTO.</param>
        /// <param name="appointmentsType">Upcoming or Past.</param>
        /// <returns>Expression based on given appointments type and other criterias.</returns>
        private Expression<Func<Appointment, bool>> BuildExpressionForAppointmentsFiltering(AppointmentInfoCriteriaDTO criterias, string appointmentsType)
        {
            var specialistName = NormalizedSpecialistName(criterias.SpecialistName);
            var fromDate = GetFromDate(criterias.From, criterias.PatientLocalDate, appointmentsType);
            var toDate = GetToDate(criterias.To, appointmentsType);

            Expression<Func<Appointment, bool>> expression
                = a => a.Patient.UserId == criterias.UserId
               && (string.IsNullOrEmpty(specialistName) ? true : (a.Doctor.FirstName + a.Doctor.LastName).StartsWith(specialistName))
               && (string.IsNullOrEmpty(criterias.SpecialtyName) ? true : a.Doctor.Specialty.Name == criterias.SpecialtyName)
               && (string.IsNullOrEmpty(fromDate) ? (appointmentsType == AppointmentsType.Upcoming ? a.ScheduledTime >= DateTime.UtcNow : a.ScheduledTime <= DateTime.UtcNow) : a.ScheduledTime >= DateTime.Parse(fromDate))
               && (string.IsNullOrEmpty(toDate) ? (appointmentsType == AppointmentsType.Past ? a.ScheduledTime <= DateTime.UtcNow : true) : a.ScheduledTime <= DateTime.Parse(toDate));

            return expression;
        }

        /// <summary>
        /// Private method to validate SpecialistName.
        /// </summary>
        /// <param name="specialistName">Specialist's name.</param>
        /// <returns>String representing the Specialist Name or empty string.</returns>
        private string NormalizedSpecialistName(string specialistName)
        {
            // Any whitespaces are removed from the specialist name.
            var result =
                string.IsNullOrEmpty(specialistName)
                ? string.Empty
                : Regex.Replace(specialistName, @"\s", string.Empty);

            return result;
        }

        /// <summary>
        /// Private method to validate To date.
        /// </summary>
        /// <param name="toDate">Date to be validated.</param>
        /// <param name="appointmentsType">Type fo the appointments.</param>
        /// <returns>String representing a valid Date or empty string.</returns>
        private string GetToDate(string toDate, string appointmentsType)
        {
            DateTime to = new DateTime();

            bool hasToSucceded = false;

            // Check if the patient has given a criteria for the To date and tries to parse it.
            hasToSucceded = DateTime.TryParse(toDate, out to);

            // Checks if To date has succeeded and the Patient has entered a date bigger than today's date.
            if (hasToSucceded && to > DateTime.UtcNow && appointmentsType == AppointmentsType.Past)
            {
                to = DateTime.UtcNow;
            }

            // Check if To Filter has succeded and returns the datetime to string or empty string.
            var result = hasToSucceded
                ? to.ToString()
                : string.Empty;

            return result;
        }

        /// <summary>
        /// Private method to validate From date.
        /// </summary>
        /// <param name="fromDate">Date to be validated.</param>
        /// <param name="patientLocalDate">Patient Local Date</param>
        /// <param name="appointmentsType">Type of the appointments.</param>
        /// <returns>String representing a valid Date or empty string.</returns>
        private string GetFromDate(string fromDate, string patientLocalDate, string appointmentsType)
        {

            DateTime from = new DateTime();
            DateTime patientLocalDateTime = new DateTime();

            bool hasPatientLocalDateSucceded = false;
            bool hasFromSucceded = false;

            // Check if the patient's local/browser date is passed from the FE and tries to parse it.
            hasPatientLocalDateSucceded = DateTime.TryParse(patientLocalDate, out patientLocalDateTime);

            // Check if the patient has given a criteria for the From date.
            // Tries to parse the given Date.
            hasFromSucceded = DateTime.TryParse(fromDate, out from);

            // If From date has not succeeded and patient local/browser date is passed and parsed correctly assigns the patient local/browser date to From date.
            if (!hasFromSucceded && hasPatientLocalDateSucceded)
            {
                from = patientLocalDateTime;
            }
            // Otherwise if the first statement has failed we check whether the user has entered a Date earlier than today to check for upcoming appointments.
            else if (appointmentsType == AppointmentsType.Upcoming && from < DateTime.UtcNow)
            {
                from = DateTime.UtcNow;
            }
            // Otherwise if the previous statement has failed we check whether the appointment type is past and user has entered a Date later than today to check for past appointments.
            else if (appointmentsType == AppointmentsType.Past && from > DateTime.UtcNow)
            {
                from = DateTime.UtcNow;
            }

            // string.Empty is passed if nothing success on all three From / To / Patient Local Date
            // as the checking in the AppointmentRepository is based on whether the properties are either null or empty.
            // Check if either From filter or Patient local/browser Date has succeded and returns the datetime to string or empty string.
            string result = hasFromSucceded || hasPatientLocalDateSucceded
                ? from.ToString()
                : string.Empty;

            return result;
        }
    }
}
