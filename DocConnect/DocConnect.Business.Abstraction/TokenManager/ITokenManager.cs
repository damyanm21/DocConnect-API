using DocConnect.Business.Models.Token;

namespace DocConnect.Business.Abstraction.TokenManager
{
    /// <summary>
    /// Class which contains the logic for Token CRUD operations.
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// Method to check whether the user has an active token or not.
        /// </summary>
        /// <param name="userId">User Identificator.</param>
        /// <param name="tokenType">Token Type.</param>
        /// <returns>True/False whether there is an active token or not.</returns>
        bool IsActiveTokenExisting(string userId, string tokenType);

        /// <summary>
        /// Method to add a user's token.
        /// </summary>
        /// <param name="token">Token object.</param>
        void AddToken(InMemoryToken token);

        /// <summary>
        /// Method to delete an used token by the User.
        /// </summary>
        /// <param name="userId">User Identificator.</param>
        /// <param name="tokenValue">Token Value.</param>
        void DeleteToken(string userId, string tokenValue);
    }
}

