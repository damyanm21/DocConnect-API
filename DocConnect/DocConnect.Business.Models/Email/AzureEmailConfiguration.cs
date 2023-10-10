using static DocConnect.Data.Models.Utilities.Constants.EnvironmentalVariablesConstants;

namespace DocConnect.Business.Models.Email
{
    /// <summary>
    /// Class used for Azure Email configuration.
    /// </summary>
    public class AzureEmailConfiguration
    {
        /// <summary>
        /// Endpoint.
        /// </summary>
        public string Endpoint { get; set; }
                        = Environment.GetEnvironmentVariable(AzureSmtpEndpoint);

        /// <summary>
        /// Identificator Key.
        /// </summary>
        public string Key { get; set; }
                        = Environment.GetEnvironmentVariable(AzureSmtpKey);

        /// <summary>
        /// Azure connection string.
        /// </summary>
        public string ConnectionString { get; set; }
                     = Environment.GetEnvironmentVariable(AzureSmtpConnectionString);

        /// <summary>
        /// Sender Email.
        /// </summary>
        public string EmailSender { get; set; }
                     = Environment.GetEnvironmentVariable(AzureSmtpSender);
    }
}

