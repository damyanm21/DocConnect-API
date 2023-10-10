using DocConnect.Business.Models.Specialty;
using DocConnect.Business.Services;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;
using Moq;

namespace DocConnect.Tests.ServicesTests
{
    [TestFixture]
    public class SpecialtyServiceTests
    {
        private SpecialtyService _service;
        private Mock<ISpecialtyRepository> _specialtyRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _specialtyRepositoryMock = new Mock<ISpecialtyRepository>();

            _service = new SpecialtyService(_specialtyRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllAsync_WhenNoErrorsAreThrown_ReturnsAllSpecialtiesInAlphabeticalOrderAscendingSuccessfully()
        {
            //Arange
            var specialtyDomainObjects = new List<Specialty>
            {
                new Specialty
                {
                    Id = 1,
                    Name = "TestName",
                    ImageUrl = string.Empty
                },
                 new Specialty
                {
                    Id = 2,
                    Name = "ATestName",
                    ImageUrl = string.Empty
                },
            };
            _specialtyRepositoryMock
                .Setup(r => r.AllReadonly())
                .ReturnsAsync(specialtyDomainObjects);

            var specialtyDTOs = new List<SpecialtyGetDTO>
            {
                new SpecialtyGetDTO
                {
                    Id = 1,
                    Name = "TestName",
                    ImageUrl = string.Empty
                },
                  new SpecialtyGetDTO
                {
                    Id = 2,
                    Name = "ATestName",
                    ImageUrl = string.Empty
                }
            };

            //Act
            var result = (await _service.GetAllAsync()).ToList();

            //Assert
            var firstSpecialty = result[0];

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(firstSpecialty.Name, Is.EqualTo("ATestName"));
            Assert.That(firstSpecialty.Id, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllAsync_WhenRepositoryFails_ReturnsNull()
        {
            //Arange
            _specialtyRepositoryMock
                .Setup(r => r.AllReadonly())
                .ThrowsAsync(new Exception());

            //Act & Assert
            Assert.ThrowsAsync<Exception>(_service.GetAllAsync);
        }
    }
}
