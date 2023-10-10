namespace DocConnect.Business.Models.Token
{
    /// <summary>
    /// Class that represent an in-memory token.
    /// </summary>
    public class InMemoryToken
    {
        /// <summary>
        /// User Identificator.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Token Type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Token Value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Expiration time of the Token.
        /// </summary>
        public DateTime Expiry { get; set; }
    }
}

