using DocConnect.Data.Models.Domains;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Class which contains the logic for Token processing.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Async method to check and generate a Token for a certain User.
        /// </summary>
        /// <param name="user">Applicaiton User.</param>
        /// <param name="tokenType">Token Type.param>
        /// <returns>Token or null if an active token exists.</returns>
        Task<string> GenerateTokenAsync(ApplicationUser user, string tokenType);

        /// <summary>
        /// Async method to validate a Token for a certain User.
        /// </summary>
        /// <param name="user">Applicaiton User.</param>
        /// <param name="token">Forot Password Token.</param>
        /// <param name="token">Token Type.</param>
        /// <returns>True/False whether the Token is valid.</returns>
        Task<bool> ValidateTokenAsync(ApplicationUser user, string token, string tokenType);

        /// <summary>
        /// Method to delete an used token by the User.
        /// </summary>
        /// <param name="userId">User Identificator.</param>
        /// <param name="tokenValue">Token Value.</param>
        void DeleteToken(string userId, string tokenValue);
    }
}

