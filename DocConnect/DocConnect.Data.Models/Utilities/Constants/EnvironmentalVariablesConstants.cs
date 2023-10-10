namespace DocConnect.Data.Models.Utilities.Constants
{
    /// <summary>
    /// Static class used Environmental Variables Constants.
    /// </summary>
    public static class EnvironmentalVariablesConstants
    {
        public const string AzureSmtpEndpoint = "AZURE_SMTP_ENDPOINT";

        public const string AzureSmtpKey = "AZURE_SMTP_KEY";

        public const string AzureSmtpConnectionString = "AZURE_SMTP_CONNECTION_STRING";

        public const string AzureSmtpSender = "AZURE_SMTP_SENDER";

        public const string AzureImageDomain = "AZURE_IMAGE_DOMAIN";

        public const string JwtSecret = "JWTSettings:Secret";

        public const string JwtExpireTime = "JWTSettings:ExpireTime";

        public const string JwtIssuer = "JWTSettings:Issuer";

        public const string JwtAudience = "JWTSettings:Audience";
    }
}