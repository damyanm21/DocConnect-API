using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.Helpers.ResponseResult;
using DocConnect.Business.Models.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseModel>> SignUp(UserSignUpInfoDTO userInfo)
        {
            var response = await _userService.SignUpAsync(userInfo, Request.Headers["Origin"].FirstOrDefault());

            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseModel>> LogIn(UserLogInInfoDTO userInfo)
        {
            var response = await _userService.LogInAsync(userInfo);

            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpGet("SendEmailVerification")]
        public async Task<ActionResult<ResponseModel>> SendEmailVerification(string email)
        {
            var response = await _userService.SendEmailVerificationAsync(email, Request.Headers["Origin"].FirstOrDefault());

            return StatusCode(response.HttpStatusCode, response);
        }

        [Authorize]
        [HttpGet("ResendEmailVerification")]
        public async Task<ActionResult<ResponseModel>> ResendEmailVerification()
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"];

            var response = await _userService.ResendEmailVerificationAsync(jwtToken, Request.Headers["Origin"].FirstOrDefault());

            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPost("ConfirmUserEmail")]
        public async Task<ActionResult<ResponseModel>> ConfirmUserEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var response = await _userService.ConfirmEmailAsync(userId, token);

            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPost("RequestForgottenPassword")]
        public async Task<ActionResult<ResponseModel>> RequestForgottenPassword(UserEmailDTO userEmail)
        {
            var response = await _userService.RequestForgottenPasswordAsync(userEmail.EmailAddress, Request.Headers["Origin"].FirstOrDefault());
            
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPost("RequestPasswordReset")]
        public async Task<ActionResult<ResponseModel>> RequestPasswordReset(UserRequestPasswordResetDTO userForgottenPassword)
        {
            var response = await _userService.RequestPasswordResetAsync(userForgottenPassword);

            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ResponseModel>> ResetPassword([FromQuery] string userId, [FromQuery] string token, [FromBody] UserPasswordResetDTO resetDTO)
        {
            var response = await _userService.ResetPasswordAsync(userId, token, resetDTO.NewPassword);

            return StatusCode(response.HttpStatusCode, response);
        }
    }
}