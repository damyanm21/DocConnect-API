using System.Linq.Expressions;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.AppointmentDTOs;
using DocConnect.Business.Models.LocationDTOs;
using DocConnect.Business.Models.Structs;
using DocConnect.Business.Services;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;
using FluentAssertions;
using Moq;
using NUnit.Framework.Internal;
using static DocConnect.Business.Utilities.ErrorMessages;

namespace DocConnect.Tests.ServicesTests
{
    [TestFixture]
    public class AppointmentServiceTests
    {
        private Mock<IAppointmentRepository> _appointmentRepository;
        private Mock<ISpecialistRepository> _specialistRepository;
        private Mock<IPatientRepository> _patientRepository;

        private IAppointmentService _appointmentService;

        [SetUp]
        public void Setup()
        {
            _appointmentRepository = new Mock<IAppointmentRepository>();
            _specialistRepository = new Mock<ISpecialistRepository>();
            _patientRepository = new Mock<IPatientRepository>();

            _appointmentService = new AppointmentService(_appointmentRepository.Object, _specialistRepository.Object, _patientRepository.Object);
        }

        [Test]
        [TestCase("2023-12-06T08:00:00")]
        [TestCase("2023-12-06T17:00:00")]
        public async Task ScheduleAnAppointmentAsync_WithInvalidHour_ReturnsBadRequestResponse(string date)
        {
            // Arange
            DateTime appointmentDate = DateTime.Parse(date);
            var dto = new AppointmentScheduleInfoDTO
            {
                Date = appointmentDate,
                DoctorId = 1,
                UserId = "1"
            };

            // Act
            var result = await _appointmentService.ScheduleAnAppointmentAsync(dto);

            // Assert
            result.HttpStatusCode.Should().Be(400);
            result.ErrorMessage.Should().Be(AppointmentHourOutOfRange);
        }

        [Test]
        public async Task ScheduleAnAppointmentAsync_WhenDoctorsAppointmentIsTaken_ReturnsBadRequestResponse()
        {
            // Arrange
            _specialistRepository
                .Setup(r => r.GetDoctorsLocationDataAsync(It.IsAny<int>()))
                .ReturnsAsync(new LocationLatitudeLongitudeDTO()
                {
                    Latitude = 41.8375m,
                    Longitude = -87.6866m
                });

            var doctorsTimezoneAppointmentDate = new DateTime(2023, 12, 07, 10, 00, 00);
            var expectedUtcConvertDate = doctorsTimezoneAppointmentDate.AddHours(5);

            _appointmentRepository
                .Setup(r => r.IsDoctorAppointmentFreeAsync(It.IsAny<int>(), expectedUtcConvertDate))
                .ReturnsAsync(false);

            var dto = new AppointmentScheduleInfoDTO
            {
                Date = doctorsTimezoneAppointmentDate,
                DoctorId = 1,
                UserId = "1"
            };

            // Act
            var result = await _appointmentService.ScheduleAnAppointmentAsync(dto);

            // Assert
            result.HttpStatusCode.Should().Be(400);
            result.ErrorMessage.Should().Be(DoctorAppointmentTaken);
        }

        [Test]
        public async Task ScheduleAnAppointmentAsync_WhenPatientAlreadyHasAppointment_ReturnsBadRequestResponse()
        {
            // Arrange
            _specialistRepository
                .Setup(r => r.GetDoctorsLocationDataAsync(It.IsAny<int>()))
                .ReturnsAsync(new LocationLatitudeLongitudeDTO()
                {
                    Latitude = 41.8375m,
                    Longitude = -87.6866m
                });

            var doctorsTimezoneAppointmentDate = new DateTime(2023, 12, 07, 10, 00, 00);
            var expectedUtcConvertDate = doctorsTimezoneAppointmentDate.AddHours(5);

            _appointmentRepository
                .Setup(r => r.IsDoctorAppointmentFreeAsync(It.IsAny<int>(), expectedUtcConvertDate))
                .ReturnsAsync(true);

            _patientRepository
                .Setup(r => r.GetPatientIdByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync(1);

            _appointmentRepository
              .Setup(r => r.IsPatientAppointmentFreeAsync(1, expectedUtcConvertDate))
              .ReturnsAsync(false);

            var dto = new AppointmentScheduleInfoDTO
            {
                Date = doctorsTimezoneAppointmentDate,
                DoctorId = 1,
                UserId = "1"
            };

            // Act
            var result = await _appointmentService.ScheduleAnAppointmentAsync(dto);

            // Assert
            result.HttpStatusCode.Should().Be(400);
            result.ErrorMessage.Should().Be(DoctorAppointmentTaken);
        }

        [Test]
        public async Task ScheduleAnAppointmentAsync_WhenAppointmentIsSuccessfulyCreated_ReturnsStatusCodeOk()
        {
            // Arrange
            _specialistRepository
                .Setup(r => r.GetDoctorsLocationDataAsync(It.IsAny<int>()))
                .ReturnsAsync(new LocationLatitudeLongitudeDTO()
                {
                    Latitude = 41.8375m,
                    Longitude = -87.6866m
                });

            var doctorsTimezoneAppointmentDate = new DateTime(2023, 10, 11, 10, 00, 00);
            var expectedUtcConvertDate = doctorsTimezoneAppointmentDate.AddHours(5);

            _appointmentRepository
                .Setup(r => r.IsDoctorAppointmentFreeAsync(It.IsAny<int>(), expectedUtcConvertDate))
                .ReturnsAsync(true);

            _patientRepository
                .Setup(r => r.GetPatientIdByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync(1);

            _appointmentRepository
              .Setup(r => r.IsPatientAppointmentFreeAsync(1, expectedUtcConvertDate))
              .ReturnsAsync(true);

            var dto = new AppointmentScheduleInfoDTO
            {
                Date = doctorsTimezoneAppointmentDate,
                DoctorId = 1,
                UserId = "1"
            };

            // Act
            var result = await _appointmentService.ScheduleAnAppointmentAsync(dto);

            // Assert
            result.HttpStatusCode.Should().Be(201);
        }

        [Test]
        public async Task AllPatientAppointmentsAsync_WithUpcomingType_ReturnsAppointmentsInCorrectOrder()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            string localDate = string.Empty;
            var appointmentsType = AppointmentsType.Upcoming;
            List<Appointment> expectedAppointments = new List<Appointment>()
            {
                new Appointment
                {
                    Id = 3,
                    Doctor = new Doctor { FirstName = "test3", LastName = "test3", Specialty = new Specialty { Name = "testSpecialty1" }, Location = new Location { CityName = "test" } },
                    Patient = new Patient { User = new ApplicationUser { FirstName = "test3", LastName = "test3" } },
                    AppointmentNotes = "test3",
                    ScheduledTime = DateTime.UtcNow
                },
                 new Appointment
                {
                    Id = 2,
                    Doctor = new Doctor { FirstName = "test2", LastName = "test2", Specialty = new Specialty { Name = "testSpecialty2" }, Location = new Location { CityName = "test" } },
                    Patient = new Patient { User = new ApplicationUser { FirstName = "test2", LastName = "test2" } },
                    AppointmentNotes = "test2",
                    ScheduledTime = DateTime.UtcNow.AddDays(1)
                },
                  new Appointment
                  {
                      Id = 1,
                    Doctor = new Doctor { FirstName = "test1", LastName = "test1", Specialty = new Specialty { Name = "testSpecialty3" }, Location = new Location { CityName = "test" } },
                    Patient = new Patient { User = new ApplicationUser { FirstName = "test1", LastName = "test1" } },
                    AppointmentNotes = "test1",
                    ScheduledTime = DateTime.UtcNow.AddDays(2)
                },
            }
            .OrderBy(a => a.ScheduledTime.Date)
            .ThenBy(a => a.ScheduledTime.Hour)
            .ToList();

            _appointmentRepository
                .Setup(ar => ar.AllPatientAppointmentsAsync(It.IsAny<Expression<Func<Appointment, bool>>>(), appointmentsType))
                .ReturnsAsync(expectedAppointments.ToList());

            // Act
            var actualAppoinments = await _appointmentService.AllPatientAppointmentsAsync(userId, localDate, appointmentsType);

            // Assert
            actualAppoinments.Should().NotBeNullOrEmpty();
            actualAppoinments.Should().HaveCount(expectedAppointments.Count());
            actualAppoinments.Skip(0).Take(1).First().Should().Match<AppointmentDTO>(a => a.Id == 3);
            actualAppoinments.Skip(1).Take(1).First().Should().Match<AppointmentDTO>(a => a.Id == 2);
            actualAppoinments.Skip(2).Take(1).First().Should().Match<AppointmentDTO>(a => a.Id == 1);
        }

        [Test]
        public async Task AllPatientAppointmentsAsync_WithPastType_ReturnsAppointmentsInCorrectOrder()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            string localDate = string.Empty;
            var appointmentsType = AppointmentsType.Past;
            List<Appointment> expectedAppointments = new List<Appointment>()
            {
                new Appointment
                {
                    Id = 3,
                    Doctor = new Doctor { FirstName = "test3", LastName = "test3", Specialty = new Specialty { Name = "testSpecialty1" }, Location = new Location { CityName = "test" }},
                    Patient = new Patient { User = new ApplicationUser { FirstName = "test3", LastName = "test3" } },
                    AppointmentNotes = "test3",
                    ScheduledTime = DateTime.UtcNow
                },
                 new Appointment
                {
                    Id = 2,
                    Doctor = new Doctor { FirstName = "test2", LastName = "test2", Specialty = new Specialty { Name = "testSpecialty2" }, Location = new Location { CityName = "test" }},
                    Patient = new Patient { User = new ApplicationUser { FirstName = "test2", LastName = "test2" } },
                    AppointmentNotes = "test2",
                    ScheduledTime = DateTime.UtcNow.AddDays(1)
                },
                  new Appointment
                {
                    Id = 1,
                    Doctor = new Doctor { FirstName = "test1", LastName = "test1", Specialty = new Specialty { Name = "testSpecialty3" }, Location = new Location { CityName = "test" }},
                    Patient = new Patient { User = new ApplicationUser { FirstName = "test1", LastName = "test1" } },
                    AppointmentNotes = "test1",
                    ScheduledTime = DateTime.UtcNow.AddDays(2)
                },
            }
            .OrderByDescending(a => a.ScheduledTime.Date)
            .ThenBy(a => a.ScheduledTime.Hour)
            .ToList();

            _appointmentRepository
                .Setup(ar => ar.AllPatientAppointmentsAsync(It.IsAny<Expression<Func<Appointment, bool>>>(), appointmentsType))
                .ReturnsAsync(expectedAppointments.ToList());

            // Act
            var actualAppoinments = await _appointmentService.AllPatientAppointmentsAsync(userId, localDate, appointmentsType);

            // Assert
            actualAppoinments.Should().NotBeNullOrEmpty();
            actualAppoinments.Should().HaveCount(expectedAppointments.Count());
            actualAppoinments.Skip(0).Take(1).First().Should().Match<AppointmentDTO>(a => a.Id == 1);
            actualAppoinments.Skip(1).Take(1).First().Should().Match<AppointmentDTO>(a => a.Id == 2);
            actualAppoinments.Skip(2).Take(1).First().Should().Match<AppointmentDTO>(a => a.Id == 3);
        }
    }
}

