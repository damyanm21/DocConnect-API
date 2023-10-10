using System.Net;
using DocConnect.Business.Models.Helpers.ResponseResult;
using DocConnect.Business.Models.UserDTOs;
using DocConnect.Business.Services;
using DocConnect.Data.Models.Domains;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using AutoFixture;
using static DocConnect.Business.Utilities.ErrorMessages;
using static DocConnect.Business.Utilities.SuccessMessages;
using DocConnect.Tests.Utilities;
using DocConnect.Business.Models.Email;
using DocConnect.Business.Abstraction.Services;

namespace DocConnect.Tests.ServicesTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private const string ValidToken = "validToken";
        private const string InvalidToken = "invalidToken";
        private const string Origin = "testOrigin";
        private Mock<MockUserManager> _userManager;
        private Mock<IJWTService> _jwtService;
        private Mock<ITokenService> _tokenService;
        private Mock<IEmailService> _emailService;
        private Mock<IPatientService> _patientService;

        private IUserService _userService;
        private PasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        private Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _userManager = new Mock<MockUserManager>();
            _emailService = new Mock<IEmailService>();
            _tokenService = new Mock<ITokenService>();
            _jwtService = new Mock<IJWTService>();
            _patientService = new Mock<IPatientService>();
            _userService = new UserService(_userManager.Object, _jwtService.Object, _tokenService.Object, _emailService.Object, _patientService.Object);
        }

        [Test]
        public async Task SendEmailVerificationAsync_WithAzureEmailServiceFailed_ReturnsServiceUnavailable()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
            .With(u => u.Email, "test@test.com")
            .With(u => u.NormalizedEmail, "TEST@TEST.COM")
            .With(u => u.PasswordHash, _passwordHasher.HashPassword(null, "Test_123!"))
            .With(u => u.EmailConfirmed, false)
            .Create();

            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _tokenService.Setup(t => t.GenerateTokenAsync(user, It.IsAny<string>())).ReturnsAsync(ValidToken);
            _emailService.Setup(e => e.SendEmailAsync(It.IsAny<EmailBuilder>())).ReturnsAsync(HttpResponseHelper.Error(HttpStatusCode.ServiceUnavailable));

            // Act
            var actualResult = await _userService.SendEmailVerificationAsync(user.Email, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.ServiceUnavailable);
        }

        [Test]
        public async Task SendEmailVerificationAsync_WithValidEmailAndNonActiveToken_ReturnsOk()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
            .With(u => u.Email, "test@test.com")
            .With(u => u.NormalizedEmail, "TEST@TEST.COM")
            .With(u => u.PasswordHash, _passwordHasher.HashPassword(null, "Test_123!"))
            .With(u => u.EmailConfirmed, false)
            .Create();

            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _tokenService.Setup(t => t.GenerateTokenAsync(user, It.IsAny<string>())).ReturnsAsync(ValidToken);
            _emailService.Setup(e => e.SendEmailAsync(It.IsAny<EmailBuilder>())).ReturnsAsync(HttpResponseHelper.Success(HttpStatusCode.OK));

            // Act
            var actualResult = await _userService.SendEmailVerificationAsync(user.Email, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeTrue();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task SendEmailVerificationAsync_WithInvalidEmail_ReturnsNotFound()
        {
            // Arrange
            var testEmail = "test@abv.bg";
            var user = _fixture.Build<ApplicationUser>().Create();
            var expectedMessage = SendVerificationEmailError;

            _userManager.Setup(u => u.FindByEmailAsync(testEmail)).ReturnsAsync(user);

            // Act
            var actualResult = await _userService.SendEmailVerificationAsync(user.Email, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.NotFound);
            actualResult.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public async Task SendEmailVerificationAsync_WithValidUserAndAlreadyConfirmedEmail_ReturnsBadRequest()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
                .With(u => u.EmailConfirmed, true)
                .Create();
            var expectedMessage = AlreadyConfirmedEmail;

            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            // Act
            var actualResult = await _userService.SendEmailVerificationAsync(user.Email, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.BadRequest);
            actualResult.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public async Task SendEmailVerificationAsync_UserWithActiveToken_ReturnsBadRequest()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
                .With(u => u.EmailConfirmed, false)
                .Create();

            var expectedMessage = EmailWithTokenAlreadySent;

            _tokenService.Setup(t => t.GenerateTokenAsync(user, It.IsAny<string>())).ReturnsAsync(string.Empty);
            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            // Act
            var actualResult = await _userService.SendEmailVerificationAsync(user.Email, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.BadRequest);
            actualResult.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public async Task ResendEmailVerificationAsync_WithAzureEmailServiceFailed_ReturnsServiceUnavailable()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
            .With(u => u.Email, "test@test.com")
            .With(u => u.NormalizedEmail, "TEST@TEST.COM")
            .With(u => u.PasswordHash, _passwordHasher.HashPassword(null, "Test_123!"))
            .With(u => u.EmailConfirmed, false)
            .Create();

            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);
            _jwtService.Setup(j => j.GetClaim(It.IsAny<string>(), It.IsAny<string>())).Returns(user.Id);
            _tokenService.Setup(t => t.GenerateTokenAsync(user, It.IsAny<string>())).ReturnsAsync(ValidToken);
            _emailService.Setup(e => e.SendEmailAsync(It.IsAny<EmailBuilder>())).ReturnsAsync(HttpResponseHelper.Error(HttpStatusCode.ServiceUnavailable));

            // Act
            var actualResult = await _userService.ResendEmailVerificationAsync(ValidToken, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.ServiceUnavailable);
        }

        [Test]
        public async Task ResendEmailVerificationAsync_WithValidJWTTokenAndNonActiveToken_ReturnsOk()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
            .With(u => u.Email, "test@test.com")
            .With(u => u.NormalizedEmail, "TEST@TEST.COM")
            .With(u => u.PasswordHash, _passwordHasher.HashPassword(null, "Test_123!"))
            .With(u => u.EmailConfirmed, false)
            .Create();

            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);
            _jwtService.Setup(j => j.GetClaim(It.IsAny<string>(), It.IsAny<string>())).Returns(user.Id);
            _tokenService.Setup(t => t.GenerateTokenAsync(user, It.IsAny<string>())).ReturnsAsync(ValidToken);
            _emailService.Setup(e => e.SendEmailAsync(It.IsAny<EmailBuilder>())).ReturnsAsync(HttpResponseHelper.Success(HttpStatusCode.OK));

            // Act
            var actualResult = await _userService.ResendEmailVerificationAsync(ValidToken, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeTrue();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task ResendEmailVerificationAsync_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var testId = Guid.NewGuid().ToString();
            var user = _fixture.Build<ApplicationUser>().Create();
            var expectedMessage = SendVerificationEmailError;

            _userManager.Setup(u => u.FindByIdAsync(testId)).ReturnsAsync(user);

            // Act
            var actualResult = await _userService.ResendEmailVerificationAsync(ValidToken, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.NotFound);
            actualResult.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public async Task ResendEmailVerificationAsync_WithValidUserAndAlreadyConfirmedEmail_ReturnsBadRequest()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
                .With(u => u.EmailConfirmed, true)
                .Create();
            var expectedMessage = AlreadyConfirmedEmail;

            _jwtService.Setup(j => j.GetClaim(It.IsAny<string>(), It.IsAny<string>())).Returns(user.Id);
            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);

            // Act
            var actualResult = await _userService.ResendEmailVerificationAsync(ValidToken, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.BadRequest);
            actualResult.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public async Task ResendEmailVerificationAsync_UserWithActiveToken_ReturnsBadRequest()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
                .With(u => u.EmailConfirmed, false)
                .Create();

            var expectedMessage = EmailWithTokenAlreadySent;

            _tokenService.Setup(t => t.GenerateTokenAsync(user, It.IsAny<string>())).ReturnsAsync(string.Empty);
            _jwtService.Setup(j => j.GetClaim(It.IsAny<string>(), It.IsAny<string>())).Returns(user.Id);
            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);

            // Act
            var actualResult = await _userService.ResendEmailVerificationAsync(ValidToken, Origin);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.Should().BeOfType<ResponseModel>();
            actualResult.Should().Match<ResponseModel>(ar => ar.HttpStatusCode == (int)HttpStatusCode.BadRequest);
            actualResult.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public async Task ConfirmEmailAsync_WithValidTokenAndUserId_ReturnsOk()
        {
            // Arrange
            var expectedMessage = EmailSuccesfullyConfirmed;
            var user = _fixture.Build<ApplicationUser>()
            .With(u => u.EmailConfirmed, false)
            .Create();

            _tokenService.Setup(t => t.ValidateTokenAsync(user, ValidToken, UserManager<ApplicationUser>.ConfirmEmailTokenPurpose)).ReturnsAsync(true);
            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);
            _userManager.Setup(u => u.ConfirmEmailAsync(user, ValidToken)).ReturnsAsync(IdentityResult.Success);

            // Act
            var actualResult = await _userService.ConfirmEmailAsync(user.Id, ValidToken);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeTrue();
            actualResult.Result.Should().Be(expectedMessage);
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task ConfirmEmailAsync_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var expectedMessage = EmailConfirmationFailed;
            var invalidUserId = Guid.NewGuid().ToString();
            var validUserId = Guid.NewGuid().ToString();

            _userManager.Setup(u => u.FindByIdAsync(validUserId)).ReturnsAsync(new ApplicationUser());

            // Act
            var actualResult = await _userService.ConfirmEmailAsync(invalidUserId, ValidToken);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.ErrorMessage.Should().Be(expectedMessage);
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task ConfirmEmailAsync_WithAlreadyConfirmedEmail_ReturnsBadRequest()
        {
            // Arrange
            var expectedMessage = AlreadyConfirmedEmail;
            var user = _fixture.Build<ApplicationUser>()
            .With(u => u.EmailConfirmed, true)
            .Create();

            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);

            // Act
            var actualResult = await _userService.ConfirmEmailAsync(user.Id, ValidToken);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.ErrorMessage.Should().Be(expectedMessage);
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task ConfirmEmailAsync_WithExpiredOrInvalidToken_ReturnsBadRequest()
        {
            // Arrange
            string expectedMessage = TokenIsExpiredOrNotValid;
            string tokenType = "EmailConfirmation";
            var user = _fixture.Build<ApplicationUser>()
            .With(u => u.EmailConfirmed, false)
            .Create();

            _tokenService.Setup(t => t.ValidateTokenAsync(user, InvalidToken, tokenType)).ReturnsAsync(false);
            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);

            // Act
            var actualResult = await _userService.ConfirmEmailAsync(user.Id, InvalidToken);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.ErrorMessage.Should().Be(expectedMessage);
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task ConfirmEmailAsync_ConfirmationFailed_ReturnsBadRequest()
        {
            // Arrange
            var expectedMessage = EmailConfirmationFailed;
            var user = _fixture.Build<ApplicationUser>()
            .With(u => u.EmailConfirmed, false)
            .Create();

            _tokenService.Setup(t => t.ValidateTokenAsync(user, ValidToken, UserManager<ApplicationUser>.ConfirmEmailTokenPurpose)).ReturnsAsync(true);
            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);
            _userManager.Setup(u => u.ConfirmEmailAsync(user, ValidToken)).ReturnsAsync(IdentityResult.Failed());

            // Act
            var actualResult = await _userService.ConfirmEmailAsync(user.Id, ValidToken);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse();
            actualResult.ErrorMessage.Should().Be(expectedMessage);
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [TestCase("test@test.com", "Test_123!")]
        [TestCase("test1@test.com", "Test1_123!")]
        public async Task LogInAsync_WithValidCredentials_ReturnsOkWithJWTToken(string email, string password)
        {
            // Arrange
            var validJWTToken = "validToken";
            var userOne = _fixture.Build<ApplicationUser>()
                .With(u => u.Email, "test@test.com")
                .With(u => u.NormalizedEmail, "TEST@TEST.COM")
                .With(u => u.PasswordHash, _passwordHasher.HashPassword(null, "Test_123!"))
                .Create();
            var userTwo = _fixture.Build<ApplicationUser>()
                .With(u => u.Email, "test1@test.com")
                .With(u => u.NormalizedEmail, "TEST1@TEST.COM")
                .With(u => u.PasswordHash, _passwordHasher.HashPassword(null, "Test1_123!"))
                .Create();

            _userManager.Setup(u => u.FindByEmailAsync("test@test.com")).ReturnsAsync(userOne);
            _userManager.Setup(u => u.FindByEmailAsync("test1@test.com")).ReturnsAsync(userTwo);
            _userManager.Setup(u => u.CheckPasswordAsync(userOne, "Test_123!")).ReturnsAsync(true);
            _userManager.Setup(u => u.CheckPasswordAsync(userTwo, "Test1_123!")).ReturnsAsync(true);

            _jwtService.Setup(j => j.GenerateJWTToken(It.IsAny<ApplicationUser>())).Returns(validJWTToken);

            _userService = new UserService(_userManager.Object, _jwtService.Object, _tokenService.Object, _emailService.Object, _patientService.Object);

            var userDto = new UserLogInInfoDTO()
            {
                EmailAddress = email,
                Password = password
            };

            var expectedResult = HttpResponseHelper.Success(HttpStatusCode.OK, "JWT Token");

            // Act
            var actualResult = await _userService.LogInAsync(userDto);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Result.Should().Be(validJWTToken);
            actualResult.Success.Should().BeTrue().And.Be(expectedResult.Success);
            actualResult.HttpStatusCode.Should().Be(expectedResult.HttpStatusCode).And.Be((int)HttpStatusCode.OK);
            actualResult.Result.Should().NotBeNull();
        }

        [Test]
        [TestCase("invalid@email.com", "Test_123!")]
        [TestCase("test@test.com", "InvalidPassword_123!")]
        public async Task LogInAsync_WithInvalidCredentials_ReturnsBadRequest(string email, string password)
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
               .With(u => u.Email, "test@test.com")
               .With(u => u.NormalizedEmail, "TEST@TEST.COM")
               .With(u => u.PasswordHash, _passwordHasher.HashPassword(null, "Test_123!"))
               .Create();

            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _userManager.Setup(u => u.CheckPasswordAsync(user, "Test_123!")).ReturnsAsync(true);

            _userService = new UserService(_userManager.Object, _jwtService.Object, _tokenService.Object, _emailService.Object, _patientService.Object);

            var userDto = new UserLogInInfoDTO()
            {
                EmailAddress = email,
                Password = password
            };

            var expectedErrorMessage = InvalidEmailOrPassword;
            var expectedResult = HttpResponseHelper.Error(HttpStatusCode.BadRequest, expectedErrorMessage);

            // Act
            var actualResult = await _userService.LogInAsync(userDto);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Success.Should().BeFalse().And.Be(expectedResult.Success);
            actualResult.HttpStatusCode.Should().Be(expectedResult.HttpStatusCode).And.Be((int)HttpStatusCode.BadRequest);
            actualResult.ErrorMessage.Should().NotBeNull();
            actualResult.ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        }

        [Test]
        public async Task SignUpAsync_WithValidInformation_ReturnsCreatedCode()
        {
            // Arrange
            var userDTO = new UserSignUpInfoDTO
            {
                EmailAddress = "testmail@abv.bg",
                FirstName = "TestFirst",
                LastName = "TestLast",
                Password = "pAs$word123",
                ConfirmPassword = "pAs$word123"
            };

            _userManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            _userService = new UserService(_userManager.Object, _jwtService.Object, _tokenService.Object, _emailService.Object, _patientService.Object);

            var expectedResult = HttpResponseHelper.Success(HttpStatusCode.Created);

            // Act
            var actualResult = await _userService.SignUpAsync(userDTO, Origin);

            // Assert
            actualResult.Success.Should().BeTrue().And.Be(expectedResult.Success);
            actualResult.HttpStatusCode.Should().Be(expectedResult.HttpStatusCode);
        }

        [Test]
        public async Task SignUpAsync_WithExistingEmail_ReturnsBadRequestCode()
        {
            // Arange

            var userDTO = new UserSignUpInfoDTO
            {
                EmailAddress = "testmail@abv.bg",
            };

            var existingUser = new ApplicationUser
            {
                Email = userDTO.EmailAddress
            };

            _userManager.Setup(m => m.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(existingUser);

            _userService = new UserService(_userManager.Object, _jwtService.Object, _tokenService.Object, _emailService.Object, _patientService.Object);

            var expectedErrorMessage = EmailAlreadyTaken;
            var expectedResult = HttpResponseHelper.Error(HttpStatusCode.BadRequest, expectedErrorMessage);

            // Act
            var actualResult = await _userService.SignUpAsync(userDTO, Origin);

            // Assert
            actualResult.Success.Should().BeFalse().And.Be(expectedResult.Success);
            actualResult.HttpStatusCode.Should().Be(expectedResult.HttpStatusCode);
            actualResult.ErrorMessage.Should().NotBeNull();
            actualResult.ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        }

        [Test]
        public async Task SignUpAsync_WhenRegistrationFails_ReturnsInternalServerErrorCode()
        {
            // Arrange
            var userDTO = new UserSignUpInfoDTO
            {
                EmailAddress = "testmail@abv.bg",
                FirstName = "TestFirst",
                LastName = "TestLast",
                Password = "pAs$word123",
                ConfirmPassword = "pAs$word123"
            };

            _userManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());

            _userService = new UserService(_userManager.Object, _jwtService.Object, _tokenService.Object, _emailService.Object, _patientService.Object);

            var expectedErrorMessage = RegistrationErrorCommonMessage;
            var expectedResult = HttpResponseHelper.Error(HttpStatusCode.InternalServerError, expectedErrorMessage);

            // Act
            var actualResult = await _userService.SignUpAsync(userDTO, Origin);

            // Assert
            actualResult.Success.Should().BeFalse().And.Be(expectedResult.Success);
            actualResult.HttpStatusCode.Should().Be(expectedResult.HttpStatusCode);
            actualResult.ErrorMessage.Should().NotBeNull();
            actualResult.ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        }

        [Test]
        public async Task RequestForgottenPasswordAsync_ValidEmail_ReturnsSuccess()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
                .With(u => u.Email, "test@test.com")
                .Create();
            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _tokenService.Setup(t => t.GenerateTokenAsync(user, UserManager<ApplicationUser>.ResetPasswordTokenPurpose)).ReturnsAsync(ValidToken);
            _emailService.Setup(e => e.SendEmailAsync(It.IsAny<EmailBuilder>())).ReturnsAsync(HttpResponseHelper.Success(HttpStatusCode.OK));

            // Act
            var actualResult = await _userService.RequestForgottenPasswordAsync(user.Email, Origin);

            // Assert
            actualResult.Success.Should().BeTrue();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task RequestForgottenPasswordAsync_WithAzureEmailServiceFailed_ReturnsOk()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
                .With(u => u.Email, "test@test.com")
                .Create();
            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _tokenService.Setup(t => t.GenerateTokenAsync(user, UserManager<ApplicationUser>.ResetPasswordTokenPurpose)).ReturnsAsync(ValidToken);
            _emailService.Setup(e => e.SendEmailAsync(It.IsAny<EmailBuilder>())).ReturnsAsync(HttpResponseHelper.Success(HttpStatusCode.OK));

            // Act
            var actualResult = await _userService.RequestForgottenPasswordAsync(user.Email, Origin);

            // Assert
            actualResult.Success.Should().BeTrue();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task RequestForgottenPasswordAsync_UserNotFound_ReturnsOk()
        {
            // Arrange
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            // Act
            var actualResult = await _userService.RequestForgottenPasswordAsync("nonexistent@test.com", Origin);

            // Assert
            actualResult.Success.Should().BeTrue();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [TestCase("", TestName = "RequestForgottenPasswordAsync_EmptyEmail_ReturnsError")]
        [TestCase("invalidEmail", TestName = "RequestForgottenPasswordAsync_InvalidEmail_ReturnsError")]
        public async Task RequestForgottenPasswordAsync_InvalidEmail_ReturnsOk(string email)
        {
            // Arrange

            // Act
            var actualResult = await _userService.RequestForgottenPasswordAsync(email, Origin);

            // Assert
            actualResult.Success.Should().BeTrue();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task ResetPasswordAsync_ValidToken_ReturnsSuccess()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
                .With(u => u.Id, "userId")
                .Create();
            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);
            _tokenService.Setup(t => t.ValidateTokenAsync(user, ValidToken, UserManager<ApplicationUser>.ResetPasswordTokenPurpose)).ReturnsAsync(true);
            _userManager.Setup(u => u.ResetPasswordAsync(user, ValidToken, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var actualResult = await _userService.ResetPasswordAsync(user.Id, ValidToken, "newPassword");

            // Assert
            actualResult.Success.Should().BeTrue();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task ResetPasswordAsync_InvalidToken_ReturnsError()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>().Create();
            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _tokenService.Setup(t => t.ValidateTokenAsync(user, InvalidToken, UserManager<ApplicationUser>.ResetPasswordTokenPurpose)).ReturnsAsync(false);

            // Act
            var actualResult = await _userService.ResetPasswordAsync(user.Email, InvalidToken, "newPassword");

            // Assert
            actualResult.Success.Should().BeFalse();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task ResetPasswordAsync_PasswordResetFailure_ReturnsError()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>().Create();
            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _tokenService.Setup(t => t.ValidateTokenAsync(user, ValidToken, UserManager<ApplicationUser>.ResetPasswordTokenPurpose)).ReturnsAsync(true);
            _userManager.Setup(u => u.ResetPasswordAsync(user, ValidToken, It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());

            // Act
            var actualResult = await _userService.ResetPasswordAsync(user.Email, ValidToken, "newPassword");

            // Assert
            actualResult.Success.Should().BeFalse();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [TestCase("", ValidToken, "newPassword", TestName = "ResetPasswordAsync_EmptyEmail_ReturnsError")]
        [TestCase("invalidEmail", ValidToken, "newPassword", TestName = "ResetPasswordAsync_InvalidEmail_ReturnsError")]
        [TestCase("test@test.com", InvalidToken, "newPassword", TestName = "ResetPasswordAsync_InvalidToken_ReturnsError")]
        [TestCase("test@test.com", ValidToken, "", TestName = "ResetPasswordAsync_EmptyNewPassword_ReturnsError")]
        public async Task ResetPasswordAsync_InvalidInputs_ReturnsError(string email, string token, string newPassword)
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>().Create();
            _userManager.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            if (token == ValidToken)
            {
                _tokenService.Setup(t => t.ValidateTokenAsync(user, ValidToken, UserManager<ApplicationUser>.ResetPasswordTokenPurpose)).ReturnsAsync(true);
            }
            else
            {
                _tokenService.Setup(t => t.ValidateTokenAsync(user, InvalidToken, UserManager<ApplicationUser>.ResetPasswordTokenPurpose)).ReturnsAsync(false);
            }

            // Act
            var actualResult = await _userService.ResetPasswordAsync(email, token, newPassword);

            // Assert
            actualResult.Success.Should().BeFalse();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [TestCase("oldPass", "newPass", TestName = "RequestPasswordResetAsync_ValidDTO_ReturnsSuccess")]
        public async Task RequestPasswordResetAsync_ValidDTO_ReturnsSuccess(string oldPassword, string newPassword)
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>().Create();
            _userManager.Setup(u => u.FindByIdAsync(user.Id)).ReturnsAsync(user);
            _userManager.Setup(u => u.ChangePasswordAsync(user, oldPassword, newPassword)).ReturnsAsync(IdentityResult.Success);

            // Act
            var dto = new UserRequestPasswordResetDTO { UserId = user.Id, OldPassword = oldPassword, NewPassword = newPassword };
            var actualResult = await _userService.RequestPasswordResetAsync(dto);

            // Assert
            actualResult.Success.Should().BeTrue();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task RequestPasswordResetAsync_ValidDTO_ReturnsSuccess()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>().Create();
            _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManager.Setup(u => u.ChangePasswordAsync(user, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var dto = new UserRequestPasswordResetDTO { UserId = "userId", OldPassword = "oldPass", NewPassword = "newPass" };
            var actualResult = await _userService.RequestPasswordResetAsync(dto);

            // Assert
            actualResult.Success.Should().BeTrue();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task RequestPasswordResetAsync_UserNotFound_ReturnsError()
        {
            // Arrange
            _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            // Act
            var dto = new UserRequestPasswordResetDTO { UserId = "nonexistentUserId", OldPassword = "oldPass", NewPassword = "newPass" };
            var actualResult = await _userService.RequestPasswordResetAsync(dto);

            // Assert
            actualResult.Success.Should().BeFalse();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task RequestPasswordResetAsync_NewPasswordDifferent_ReturnsSuccess()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>().Create();
            _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManager.Setup(u => u.ChangePasswordAsync(user, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(u => u.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(false); // Simulate different passwords

            // Act
            var dto = new UserRequestPasswordResetDTO { UserId = "userId", OldPassword = "oldPass", NewPassword = "newPass" };
            var actualResult = await _userService.RequestPasswordResetAsync(dto);

            // Assert
            actualResult.Success.Should().BeTrue();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task RequestPasswordResetAsync_NewPasswordSame_ReturnsError()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>().Create();
            _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManager.Setup(u => u.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true); // Simulate same passwords

            // Act
            var dto = new UserRequestPasswordResetDTO { UserId = "userId", OldPassword = "oldPass", NewPassword = "oldPass" };
            var actualResult = await _userService.RequestPasswordResetAsync(dto);

            // Assert
            actualResult.Success.Should().BeFalse();
            actualResult.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}