using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.Structs;
using DocConnect.Data.Models.Domains;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static DocConnect.Data.Models.Utilities.Constants.EnvironmentalVariablesConstants;

namespace DocConnect.Business.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // In the future this will be awaitable code. For now, it runs synchronously.
        public string GenerateJWTToken(ApplicationUser user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (!user.EmailConfirmed)
            {
                authClaims.Add(new Claim(CustomJWTClaimNames.EmailVerified, false.ToString()));
            }

            string jwtSecret = _configuration[JwtSecret];
            byte[] jwtSecretBytes = Encoding.UTF8.GetBytes(jwtSecret);
            var authSignKey = new SymmetricSecurityKey(jwtSecretBytes);

            int expireTimeInSeconds = int.Parse(_configuration[JwtExpireTime]);

            var token = new JwtSecurityToken(
                issuer: _configuration[JwtIssuer],
                audience: _configuration[JwtAudience],
                expires: DateTime.UtcNow.AddSeconds(expireTimeInSeconds),
                claims: authClaims,
                signingCredentials:
                         new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetClaim(string jwtToken, string claimType)
        {
            // The substring might be better to be removed and done at another level.
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken.Substring("Bearer ".Length));

            return token.Claims.FirstOrDefault(c => c.Type == claimType).Value;
        }
    }
}

