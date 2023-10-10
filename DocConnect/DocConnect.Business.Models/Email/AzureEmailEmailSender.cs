using Azure.Communication.Email;

namespace DocConnect.Business.Models.Email
{
    public class AzureEmailSender : IEmailSender 
    {
        private readonly AzureEmailConfiguration _azureEmailConfiguration;

        public AzureEmailSender(AzureEmailConfiguration azureEmailConfiguration)
        {
            _azureEmailConfiguration = azureEmailConfiguration;
        }

        public async Task<EmailSendResult> SendEmailAsync(string email, string subject, string message)
        {
            return await Execute(subject, message, email);
        }

        private async Task<EmailSendResult> Execute(string subject, string message, string email)
        {
            var client = new EmailClient(_azureEmailConfiguration.ConnectionString);

            var result = await client.SendAsync(
                senderAddress: _azureEmailConfiguration.EmailSender,
                wait: Azure.WaitUntil.Completed,
                recipientAddress: email,
                subject: subject,
                htmlContent: message,
                plainTextContent: message);

            var response = result.Value;

            return response;
        }
    }
}

