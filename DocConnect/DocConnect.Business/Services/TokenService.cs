using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Abstraction.TokenManager;
using DocConnect.Business.Models.Token;
using DocConnect.Data.Models.Domains;
using Microsoft.AspNetCore.Identity;
using static DocConnect.Data.Models.Utilities.Constants.InMemoryTokenManagerConstants;

namespace DocConnect.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ITokenManager _tokenManager;

        public TokenService(UserManager<ApplicationUser> userManager, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
        }

        public async Task<string> GenerateTokenAsync(ApplicationUser user, string tokenType)
        {
            bool isActiveTokenExisting = _tokenManager.IsActiveTokenExisting(user.Id, tokenType);

            if (isActiveTokenExisting)
            {
                return null;
            }

            var token = await GenerateToken(user, tokenType);

            _tokenManager.AddToken(new InMemoryToken
            {
                UserId = user.Id,
                Type = tokenType,
                Value = token,
                Expiry = DateTime.UtcNow.AddHours(TokenExpiryTimeInHours)
            });

            return token;
        }

        public async Task<bool> ValidateTokenAsync(ApplicationUser user, string token, string tokenType)
        {
            return await ValidateToken(user, token, tokenType);
        }

        public void DeleteToken(string userId, string tokenValue)
        {
            _tokenManager.DeleteToken(userId, tokenValue);
        }

        /// <summary>
        /// Private method to generate a token based on a given type.
        /// </summary>
        /// <param name="user">Application User.</param>
        /// <param name="tokenType">Token Type.</param>
        /// <returns>Token based on the given type.</returns>
        private async Task<string> GenerateToken(ApplicationUser user, string tokenType)
        {
            switch (tokenType)
            {
                case UserManager<ApplicationUser>.ConfirmEmailTokenPurpose:
                    return await _userManager.GenerateEmailConfirmationTokenAsync(user);
                case UserManager<ApplicationUser>.ResetPasswordTokenPurpose:
                    return await _userManager.GeneratePasswordResetTokenAsync(user);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Private method to validate a token based on a given user, token and token type.
        /// </summary>
        /// <param name="user">Application User.</param>
        /// <param name="token">Token.</param>
        /// <param name="tokenType">Token Type.</param>
        /// <returns>True/False whether the token is valid or not.</returns>
        private async Task<bool> ValidateToken(ApplicationUser user, string token, string tokenType)
        {
            switch (tokenType)
            {
                case UserManager<ApplicationUser>.ConfirmEmailTokenPurpose:
                    return await _userManager
                         .VerifyUserTokenAsync
                         (user,
                          _userManager.Options.Tokens.EmailConfirmationTokenProvider,
                          UserManager<ApplicationUser>.ConfirmEmailTokenPurpose,
                          token);
                case UserManager<ApplicationUser>.ResetPasswordTokenPurpose:
                    return await _userManager
                         .VerifyUserTokenAsync
                         (user,
                          _userManager.Options.Tokens.PasswordResetTokenProvider,
                          UserManager<ApplicationUser>.ResetPasswordTokenPurpose,
                          token);
                default:
                    return false;
            }
        }
    }
}