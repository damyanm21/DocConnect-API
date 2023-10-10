using System.Net;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.Email;
using DocConnect.Business.Models.Helpers.ResponseResult;
using DocConnect.Business.Models.UserDTOs;
using DocConnect.Data.Models.Domains;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using static DocConnect.Business.Utilities.ErrorMessages;
using static DocConnect.Business.Utilities.SuccessMessages;
using static DocConnect.Data.Models.Utilities.Constants.EmailBuilderConstants;
using static DocConnect.Data.Models.Utilities.Constants.ApplicationUserConstants;

namespace DocConnect.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IJWTService _jwtService;

        private readonly ITokenService _tokenService;

        private readonly IEmailService _emailService;

        private readonly IPatientService _patientService;

        public UserService(UserManager<ApplicationUser> userManager,
                           IJWTService jwtService,
                           ITokenService tokenService,
                           IEmailService emailService,
                           IPatientService patientService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _tokenService = tokenService;
            _emailService = emailService;
            _patientService = patientService;
        }

        public async Task<ResponseModel> SignUpAsync(UserSignUpInfoDTO userInfo, string origin)
        {
            var existingEmail = await _userManager.FindByEmailAsync(userInfo.EmailAddress);

            if (existingEmail != null)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, EmailAlreadyTaken);
            }

            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = userInfo.EmailAddress,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                UserName = userInfo.EmailAddress,
            };

            var result = await _userManager.CreateAsync(newUser, userInfo.Password);

            if (result.Succeeded)
            {
                await _patientService.AddAsync(new Patient() { UserId = newUser.Id });

                await SendEmailVerificationAsync(newUser.Email, origin);

                return HttpResponseHelper
                    .Success(HttpStatusCode.Created);
            }
            else
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.InternalServerError, RegistrationErrorCommonMessage);
            }
        }

        public async Task<ResponseModel> LogInAsync(UserLogInInfoDTO userInfo)
        {
            var user = await _userManager.FindByEmailAsync(userInfo.EmailAddress);

            if (user == null)
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.BadRequest, InvalidEmailOrPassword);
            }

            var result = await _userManager.CheckPasswordAsync(user, userInfo.Password);

            if (!result)
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.BadRequest, InvalidEmailOrPassword);
            }

            var token = _jwtService.GenerateJWTToken(user);

            return HttpResponseHelper
                .Success(HttpStatusCode.OK, token);
        }

        public async Task<ResponseModel> SendEmailVerificationAsync(string email, string origin)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return await SendVerificationEmailAsync(user, origin);
        }

        public async Task<ResponseModel> ResendEmailVerificationAsync(string jwtToken, string origin)
        {
            string userId = _jwtService.GetClaim(jwtToken, JwtRegisteredClaimNames.Sub);

            var user = await _userManager.FindByIdAsync(userId);

            return await SendVerificationEmailAsync(user, origin);
        }

        public async Task<ResponseModel> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.BadRequest, EmailConfirmationFailed);
            }

            if (user.EmailConfirmed)
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.BadRequest, AlreadyConfirmedEmail);
            }

            bool isTokenValid = await _tokenService.ValidateTokenAsync(user, token, UserManager<ApplicationException>.ConfirmEmailTokenPurpose);

            if (!isTokenValid)
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.BadRequest, TokenIsExpiredOrNotValid);
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.BadRequest, EmailConfirmationFailed);
            }

            _tokenService.DeleteToken(user.Id, token);

            return HttpResponseHelper
                   .Success(HttpStatusCode.OK, EmailSuccesfullyConfirmed);
        }

        public async Task<ResponseModel> RequestForgottenPasswordAsync(string email, string origin)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return HttpResponseHelper.Success(HttpStatusCode.OK);
            }

            var token = await _tokenService.GenerateTokenAsync(user, UserManager<ApplicationUser>.ResetPasswordTokenPurpose);

            if (token == null)
            {
                return HttpResponseHelper.Success(HttpStatusCode.OK);
            }

            var resetLink = string.Format(ForgottenPasswordResetUrlBuilder, origin, user.Id, token);

            var emailBuilder = new EmailBuilder
            {
                Email = email,
                Subject = ForgottenPasswordSubject,
                Message = string.Format(ForgottenPasswordMessage, user.FirstName, user.LastName, resetLink)
            };

            var result = await _emailService.SendEmailAsync(emailBuilder);

            if (!result.Success)
            {
                _tokenService.DeleteToken(user.Id, token);
            }

            return HttpResponseHelper.Success(HttpStatusCode.OK);
        }

        public async Task<ResponseModel> RequestPasswordResetAsync(UserRequestPasswordResetDTO passwordResetDTO)
        {
            var user = await _userManager.FindByIdAsync(passwordResetDTO.UserId);

            if (user == null)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, UserNotFound);
            }

            var isSamePassword = await _userManager.CheckPasswordAsync(user, passwordResetDTO.NewPassword);

            if (isSamePassword)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, NewPasswordSameAsOld);
            }

            var passwordChangeResult = await _userManager.ChangePasswordAsync(user, passwordResetDTO.OldPassword, passwordResetDTO.NewPassword);

            if (!passwordChangeResult.Succeeded)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, PasswordResetFailed);
            }

            return HttpResponseHelper.Success(HttpStatusCode.OK);
        }

        public async Task<ResponseModel> ResetPasswordAsync(string id, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, UserNotFound);
            }

            var isValidToken = await _tokenService.ValidateTokenAsync(user, token, UserManager<ApplicationUser>.ResetPasswordTokenPurpose);

            if (!isValidToken)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, TokenIsExpiredOrNotValid);
            }

            var isSamePassword = await _userManager.CheckPasswordAsync(user, newPassword);

            if (isSamePassword)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, NewPasswordSameAsOld);
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                return HttpResponseHelper.Error(HttpStatusCode.BadRequest, PasswordResetFailed);
            }

            _tokenService.DeleteToken(user.Id, token);

            return HttpResponseHelper.Success(HttpStatusCode.OK, PasswordSuccesfullyReset);
        }

        private async Task<ResponseModel> SendVerificationEmailAsync(ApplicationUser user, string origin)
        {
            if (user == null)
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.NotFound, SendVerificationEmailError);
            }

            if (user.EmailConfirmed)
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.BadRequest, AlreadyConfirmedEmail);
            }

            var token = await _tokenService.GenerateTokenAsync(user, UserManager<ApplicationUser>.ConfirmEmailTokenPurpose);

            if (string.IsNullOrEmpty(token))
            {
                return HttpResponseHelper
                    .Error(HttpStatusCode.BadRequest, EmailWithTokenAlreadySent);
            }

            var resetLink = string.Format(EmailVerificationUrlBuilder, origin, user.Id, token);

            var emailBuilder = new EmailBuilder()
            {
                Email = user.Email,
                Subject = EmailVerificationSubject,
                Message = string.Format(EmailVerificationMessage, user.FirstName, user.LastName, resetLink)
            };

            var result = await _emailService.SendEmailAsync(emailBuilder);

            if (!result.Success)
            {
                _tokenService.DeleteToken(user.Id, token);
            }

            return result;
        }
    }
}

