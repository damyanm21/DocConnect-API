using DocConnect.Business.Models.Helpers.ResponseResult;
using DocConnect.Business.Models.UserDTOs;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Contains the business logic related to the users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates a new user for the application.
        /// </summary>
        /// <param name="userInfo">An object containing inforamtion required for the sign up.</param>
        /// <param name="origin">Url which is retrieved from the request headers. Used for URL construction.</param>
        /// <returns></returns>
        Task<ResponseModel> SignUpAsync(UserSignUpInfoDTO userInfo, string origin);

        /// <summary>
        /// Checks for existing user in the application.
        /// </summary>
        /// <param name="userInfo">An object containing inforamtion required for the log in.</param>
        /// <returns></returns>
        Task<ResponseModel> LogInAsync(UserLogInInfoDTO userInfo);

        /// <summary>
        /// Sends an email for email verification.
        /// </summary>
        /// <param name="email">Email corresponding to an account that needs to be activated.</param>
        /// <param name="origin">Url which is retrieved from the request headers. Used for URL construction.</param>
        /// <returns></returns>
        Task<ResponseModel> SendEmailVerificationAsync(string email, string origin);

        /// <summary>
        /// Resends an email for email verification.
        /// </summary>
        /// <param name="jwtToken">JWT Token used to authenticate the user and get its claims.</param>
        /// <param name="origin">Url which is retrieved from the request headers. Used for URL construction.</param>
        /// <returns></returns>
        Task<ResponseModel> ResendEmailVerificationAsync(string jwtToken, string origin);

        /// <summary>
        /// Confirms an user's email.
        /// </summary>
        /// <param name="userId">User Identificator used to retrieve the User..</param>
        /// <param name="token">Token used to confirm the email and check whether the correct user made the request.</param>
        /// <returns></returns>
        Task<ResponseModel> ConfirmEmailAsync(string userId, string token);

        /// <summary>
        /// Sends an email to the user, which contains the url, navigating to the password reset.
        /// </summary>
        /// <param name="email">The email address for which password reset is requested.</param>
        /// <param name="origin">Url which is retrieved from the request headers. Used for URL construction.</param>
        /// <returns></returns>
        Task<ResponseModel> RequestForgottenPasswordAsync(string email, string origin);

        /// <summary>
        /// Sends a request to reset the password associated with the provided user data.
        /// </summary>
        /// <param name="passwordResetDTO">The DTO holding the data for the password reset.</param>
        /// <returns></returns>
        Task<ResponseModel> RequestPasswordResetAsync(UserRequestPasswordResetDTO passwordResetDTO);

        /// <summary>
        /// Resets the password for the specified email address using the provided token and new password.
        /// </summary>
        /// <param name="id">The id for which password reset is being performed.</param>
        /// <param name="token">The token associated with the password reset request.</param>
        /// <param name="newPassword">The new password to be set after the reset.</param>
        /// <returns></returns>
        Task<ResponseModel> ResetPasswordAsync(string id, string token, string newPassword);
    }
}
