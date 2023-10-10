using DocConnect.Data.Models.Domains;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Service class to help with JWT processing.
    /// </summary>
    public interface IJWTService
    {
        /// <summary>
        /// Method to generate a JWT Token.
        /// </summary>
        /// <param name="user">ApplicationUser used for Claims.</param>
        /// <returns>JWT Token as string.</returns>
        string GenerateJWTToken(ApplicationUser user);

        /// <summary>
        /// Method to retrieve an User Claim..
        /// </summary>
        /// <param name="jwtToken">JWT Token used to authenticate and retrieve the user claims.</param>
        /// <param name="claimType">Claim Type used to retrieve a single user Claim.</param>
        /// <returns>Claim retrieved by the given claim type.</returns>
        string GetClaim(string jwtToken, string claimType);
    }
}

