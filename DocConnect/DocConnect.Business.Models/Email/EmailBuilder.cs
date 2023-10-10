using System.Text.Json.Serialization;

namespace DocConnect.Business.Models.Email
{
    /// <summary>
    /// EmailBuilder class used for email configuration.
    /// </summary>
    public class EmailBuilder
    {
        /// <summary>
        /// Receiver Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Subject of the Email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Body of the Email.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// HTML Template of the Email.
        /// </summary>
        [JsonIgnore]
        public string? Template { get; set; }
    }
}

