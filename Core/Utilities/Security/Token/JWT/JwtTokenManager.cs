using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Token.JWT
{
    public class JwtTokenManager : ITokenService
    {
        private AppSettings _appSettings;
        public JwtTokenManager(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public AccessToken CreateToken(int userId, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                    new Claim(ClaimTypes.Name,userName.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AccessToken()
            {
                Token = tokenHandler.WriteToken(token),
                UserName = userName,
                UserId = userId,
                Expiration = (DateTime)tokenDescriptor.Expires
            };
        }
    }
}
