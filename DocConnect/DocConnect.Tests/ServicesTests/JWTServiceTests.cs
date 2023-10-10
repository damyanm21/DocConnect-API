using System.IdentityModel.Tokens.Jwt;
using AutoFixture;
using DocConnect.Business.Services;
using DocConnect.Data.Models.Domains;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using static DocConnect.Data.Models.Utilities.Constants.EnvironmentalVariablesConstants;

namespace DocConnect.Tests.ServicesTests
{
    [TestFixture]
    public class JWTServiceTests
    {
        private const string JwtSecretValue = "Test1Test2Test3Test4";
        private const string JwtExpireTimeValue = "1440";
        private const string JwtAudienceValue = "TestAudience";
        private const string JwtIssuerValue = "TestIssuer";

        private Mock<IConfiguration> _configuration;

        private JWTService _jwtService;

        private Fixture _fixutre = new Fixture();

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();

            _configuration.Setup(c => c[JwtSecret]).Returns(JwtSecretValue);
            _configuration.Setup(c => c[JwtExpireTime]).Returns(JwtExpireTimeValue);
            _configuration.Setup(c => c[JwtAudience]).Returns(JwtAudienceValue);
            _configuration.Setup(c => c[JwtIssuer]).Returns(JwtIssuerValue);

            _jwtService = new JWTService(_configuration.Object);
        }

        [Test]
        public void GenerateJWTToken_WithValidUser_ReturnsToken()
        {
            // Arrange
            var user = _fixutre.Build<ApplicationUser>().Create();
            int expectedPartsCount = 3;

            // Act
            var token = _jwtService.GenerateJWTToken(user);

            // Assert
            int actualPartsCount = token.Split('.').ToArray().Count();
            token.Should().NotBeNull();
            actualPartsCount.Should().Be(expectedPartsCount);
        }

        [Test]
        public void GenerateJWTToken_WithInvalidConfiguration_ThrowsException()
        {
            // Arrange
            _configuration.Reset();
            var user = _fixutre.Build<ApplicationUser>().Create();

            // Act
            Action act = () => _jwtService.GenerateJWTToken(user);

            // Assert
            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetClaim_WithTokenAndClaimType_ReturnsCorrectClaim()
        {
            // Arrange
            var authorizationType = "Bearer ";
            var user = _fixutre.Build<ApplicationUser>().Create();
            string claimType = JwtRegisteredClaimNames.Sub;
            string expectedClaimValue = user.Id;

            // Act
            string jwtToken = string.Concat(authorizationType, _jwtService.GenerateJWTToken(user));
            var actualClaimValue = _jwtService.GetClaim(jwtToken, claimType);

            // Assert
            actualClaimValue.Should().NotBeNull();
            actualClaimValue.Should().Be(expectedClaimValue);
        }
    }
}

