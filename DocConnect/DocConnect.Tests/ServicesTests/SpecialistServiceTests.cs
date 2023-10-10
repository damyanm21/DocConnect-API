using DocConnect.Business.Services;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;
using FluentAssertions;
using Moq;

namespace DocConnect.Tests.ServicesTests
{
    [TestFixture]
    public class SpecialistServiceTests
    {
        private SpecialistService _specialistService;
        private Mock<ISpecialistRepository> _specialistRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _specialistRepositoryMock = new Mock<ISpecialistRepository>();
            _specialistService = new SpecialistService(_specialistRepositoryMock.Object);
        }

        [Test]
        public async Task GetSpecialistByIdAsync_WhenSpecialistExists_ReturnsSpecialistDetailedInfoDTO()
        {
            // Arrange
            int specialistId = 1;
            var expectedSpecialist = new Doctor
            {
                Id = specialistId,
                FirstName = "John",
                LastName = "Doe",
                Specialty = new Specialty { Name = "Cardiology" },
                Address = "123 Main St",
                Location = new Location { CityName = "City" }, // Add Location
                DoctorSummary = "Summary", // Add DoctorSummary
                Ratings = new List<Rating> { new Rating { RatingPoints = 4 } }
            };

            _specialistRepositoryMock.Setup(repo => repo.GetSpecialistInfoByIdAsync(specialistId)).ReturnsAsync(expectedSpecialist);

            // Act
            var result = await _specialistService.GetSpecialistByIdAsync(specialistId);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(expectedSpecialist.FirstName);
            result.LastName.Should().Be(expectedSpecialist.LastName);
            result.SpecialityName.Should().Be(expectedSpecialist.Specialty.Name);
            result.Address.Should().Be($"{expectedSpecialist.Address}, {expectedSpecialist.Location.CityName}");
            result.DoctorSummary.Should().Be(expectedSpecialist.DoctorSummary);
            result.Rating.Should().Be(expectedSpecialist.Ratings.FirstOrDefault()?.RatingPoints);
        }

        [Test]
        public async Task GetSpecialistByIdAsync_WhenSpecialistDoesNotExist_ReturnsNull()
        {
            // Arrange
            int specialistId = 2;
            _specialistRepositoryMock.Setup(repo => repo.GetSpecialistInfoByIdAsync(specialistId)).ReturnsAsync((Doctor)null);

            // Act
            var result = await _specialistService.GetSpecialistByIdAsync(specialistId);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task GetSpecialistByIdAsync_WhenNoRatingsExist_ReturnsRatingZero()
        {
            // Arrange
            int specialistId = 3;
            var specialist = new Doctor
            {
                Id = specialistId,
                FirstName = "Alice",
                LastName = "Smith",
                Specialty = new Specialty { Name = "Dermatology" },
                Address = "456 Elm St",
                Location = new Location { CityName = "City" }, // Add Location
                DoctorSummary = "Summary", // Add DoctorSummary
                Ratings = new List<Rating>()
            };
            _specialistRepositoryMock.Setup(repo => repo.GetSpecialistInfoByIdAsync(specialistId)).ReturnsAsync(specialist);

            // Act
            var result = await _specialistService.GetSpecialistByIdAsync(specialistId);

            // Assert
            result.Should().NotBeNull();
            result.Rating.Should().Be(0);
        }


        [Test]
        public async Task GetSpecialistByIdAsync_WhenSpecialistExistsWithNoRatings_ReturnsRatingZero()
        {
            // Arrange
            int specialistId = 2;
            var specialist = new Doctor
            {
                Id = specialistId,
                FirstName = "Alice",
                LastName = "Smith",
                Specialty = new Specialty { Name = "Dermatology" },
                Address = "456 Elm St",
                Location = new Location { CityName = "City" }, // Add Location
                DoctorSummary = "Summary", // Add DoctorSummary
                Ratings = new List<Rating>()
            };
            _specialistRepositoryMock.Setup(repo => repo.GetSpecialistInfoByIdAsync(specialistId)).ReturnsAsync(specialist);

            // Act
            var result = await _specialistService.GetSpecialistByIdAsync(specialistId);

            // Assert
            result.Should().NotBeNull();
            result.Rating.Should().Be(0);
        }
    }
}
