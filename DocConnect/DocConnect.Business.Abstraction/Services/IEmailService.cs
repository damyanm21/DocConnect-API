using DocConnect.Business.Models.Email;
using DocConnect.Business.Models.Helpers.ResponseResult;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Contains the business logic related to the emails.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Method used to send an email to the provided email.
        /// </summary>
        /// <returns></returns>
        Task<ResponseModel> SendEmailAsync(EmailBuilder emailBuilder);
    }
}

