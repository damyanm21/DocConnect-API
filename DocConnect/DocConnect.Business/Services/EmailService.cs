using System.Net;
using Azure.Communication.Email;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models;
using DocConnect.Business.Models.Email;
using DocConnect.Business.Models.Helpers.ResponseResult;
using static DocConnect.Business.Utilities.ErrorMessages;

namespace DocConnect.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _azureEmailSender;

        public EmailService(IEmailSender azureEmailSender)
        {
            _azureEmailSender = azureEmailSender;
        }

        public async Task<ResponseModel> SendEmailAsync(EmailBuilder emailBuilder)
        {
            var result = await _azureEmailSender.SendEmailAsync(
                 email: emailBuilder.Email,
                 subject: emailBuilder.Subject,
                 message: emailBuilder.Message);
            //message: string.Format(emailBuilder.Template, emailBuilder.Message));

            if (result.Status != EmailSendStatus.Succeeded)
            {
                return HttpResponseHelper.Error(HttpStatusCode.ServiceUnavailable, SendVerificationEmailError);
            }

            return HttpResponseHelper.Success(HttpStatusCode.OK);
        }
    }
}

