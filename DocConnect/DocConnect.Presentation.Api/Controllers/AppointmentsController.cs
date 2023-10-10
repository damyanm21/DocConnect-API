using System.Net;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.AppointmentDTOs;
using DocConnect.Business.Models.Helpers.ResponseResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DocConnect.Business.Models.Structs;

namespace DocConnect.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize]
        [HttpGet("AllUpcomingAppointments")]
        public async Task<ResponseModel> AllUpcomingAppointments([FromQuery] string userId, [FromQuery] string? patientLocalDate)
        {
            var dtos = await _appointmentService.AllPatientAppointmentsAsync(userId, patientLocalDate, AppointmentsType.Upcoming);

            var result = HttpResponseHelper.Success(HttpStatusCode.OK, dtos);

            return result;
        }

        [Authorize]
        [HttpPost("FilteredUpcomingAppointments")]
        public async Task<ResponseModel> FilteredUpcomingAppointments([FromBody] AppointmentInfoCriteriaDTO criterias)
        {
            var dtos = await _appointmentService.FilteredPatientAppointmentsAsync(criterias, AppointmentsType.Upcoming);

            var result = HttpResponseHelper.Success(HttpStatusCode.OK, dtos);

            return result;
        }

        [Authorize]
        [HttpPost("ScheduleAppointment")]
        public async Task<ActionResult<ResponseModel>> ScheduleAppointment([FromBody] AppointmentScheduleInfoDTO dto)
        {
            var result = await _appointmentService.ScheduleAnAppointmentAsync(dto);

            return StatusCode(result.HttpStatusCode, result);
        }

        [HttpGet("DoctorTakenHours")]
        public async Task<ActionResult<ResponseModel>> GetTakenAppointmentHoursForDoctor([FromQuery] int doctorId)
        {
            var takenHours = await _appointmentService.GetTakenAppointmentHoursForDoctorAsync(doctorId);

            return Ok(HttpResponseHelper.Success(HttpStatusCode.OK, takenHours));
        }
    }
}

