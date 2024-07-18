using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Domain.Entities;
using System.Domain.Services.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace System.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(User user)
        {
            var AuthClaim = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,user.Username),
            new Claim(ClaimTypes.Role,user.Role.ToString())
        };

            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var authcredentials = new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: AuthClaim,
                signingCredentials: authcredentials,
                expires: DateTime.Now.AddDays(1)

                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
