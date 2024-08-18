using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _JwtOptions;
        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _JwtOptions = jwtOptions.Value;
        }
        public string GenerateJwt(Guid userId, string userName)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, userName)
            }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _JwtOptions.Issuer,
                Audience = _JwtOptions.Audience,
                SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOptions.Key!)),
            SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
