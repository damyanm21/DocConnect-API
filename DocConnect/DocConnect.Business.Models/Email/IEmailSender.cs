using Azure.Communication.Email;

namespace DocConnect.Business.Models
{
    /// <summary>
    /// Class which contains the logic for configuring and sending emails with Azure.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Method used to send an Email to a given email.
        /// </summary>
        /// <param name="email">Recipient Email.</param>
        /// <param name="subject">Subject of the Email.</param>
        /// <param name="message">Body of the Email.</param>
        /// <returns>EmailSendResult with the result of email sending.</returns>
        Task<EmailSendResult> SendEmailAsync(string email, string subject, string message);
    }
}

