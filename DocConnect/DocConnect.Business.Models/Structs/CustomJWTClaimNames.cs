namespace DocConnect.Business.Models.Structs
{
    /// <summary>
    /// Struct class for custom claims.
    /// </summary>
    public struct CustomJWTClaimNames
    {
        /// <summary>
        /// Officially registered public claim used to send if the user's email is verified or not.
        /// </summary>
        public const string EmailVerified = "email_verified";
    }
}

