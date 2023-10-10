using AutoFixture;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Abstraction.TokenManager;
using DocConnect.Business.Services;
using DocConnect.Data.Models.Domains;
using DocConnect.Tests.Utilities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace DocConnect.Tests.ServicesTests
{
    public class TokenServiceTests
    {
        private Mock<MockUserManager> _userManager = new Mock<MockUserManager>();

        private Mock<ITokenManager> _tokenManager = new Mock<ITokenManager>();

        private ITokenService _tokenService;

        private Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _userManager.Reset();
            _tokenManager.Reset();
            _tokenService = new TokenService(_userManager.Object, _tokenManager.Object);
        }

        [Test]
        [TestCase(UserManager<ApplicationUser>.ConfirmEmailTokenPurpose)]
        [TestCase(UserManager<ApplicationUser>.ResetPasswordTokenPurpose)]
        public async Task GenerateTokenAsync_WithNonActiveToken_ReturnsCorrectToken(string tokenType)
        {
            // Arrange
            var expectedToken = "token";
            _userManager.Setup(um => um.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(expectedToken);
            _userManager.Setup(um => um.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(expectedToken);
            var user = _fixture.Build<ApplicationUser>().Create();

            // Act
            var actualToken = await _tokenService.GenerateTokenAsync(user, tokenType);

            // Assert
            actualToken.Should().NotBeNull();
            actualToken.Should().BeEquivalentTo(expectedToken);
        }

        [Test]
        [TestCase(UserManager<ApplicationUser>.ConfirmEmailTokenPurpose)]
        [TestCase(UserManager<ApplicationUser>.ResetPasswordTokenPurpose)]
        public async Task GenerateTokenAsync_WithActiveToken_ReturnsNull(string tokenType)
        {
            // Arrange
            _tokenManager.Setup(tm => tm.IsActiveTokenExisting(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var user = _fixture.Build<ApplicationUser>().Create();

            // Act
            var actualToken = await _tokenService.GenerateTokenAsync(user, tokenType);

            // Assert
            actualToken.Should().BeNull();
        }

        [Test]
        [TestCase(UserManager<ApplicationUser>.ConfirmEmailTokenPurpose)]
        [TestCase(UserManager<ApplicationUser>.ResetPasswordTokenPurpose)]
        public async Task ValidateTokenAsync_WithValidToken_ReturnsTrue(string tokenType)
        {
            // Arrange
            string validToken = "validToken";
            _userManager.Setup(um => um.VerifyUserTokenAsync(
                                       It.IsAny<ApplicationUser>(),
                                       It.IsAny<string>(),
                                       It.IsAny<string>(),
                                       validToken))
                .ReturnsAsync(true);
                
            var user = _fixture.Build<ApplicationUser>().Create();

            // Act
            var actualToken = await _tokenService.ValidateTokenAsync(user, validToken ,tokenType);

            // Assert
            actualToken.Should().BeTrue();
        }

        [Test]
        [TestCase(UserManager<ApplicationUser>.ConfirmEmailTokenPurpose)]
        [TestCase(UserManager<ApplicationUser>.ResetPasswordTokenPurpose)]
        public async Task ValidateTokenAsync_WithNonValidToken_ReturnsFalse(string tokenType)
        {
            // Arrange
            string validToken = "validToken";
            string invalidToken = "invalidToken";
            _userManager.Setup(um => um.VerifyUserTokenAsync(
                                       It.IsAny<ApplicationUser>(),
                                       It.IsAny<string>(),
                                       It.IsAny<string>(),
                                       validToken))
                .ReturnsAsync(true);

            var user = _fixture.Build<ApplicationUser>().Create();

            // Act
            var actualToken = await _tokenService.ValidateTokenAsync(user, invalidToken, tokenType);

            // Assert
            actualToken.Should().BeFalse();
        }
    }
}

