using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Secret_Sharing_Platform.Dto;
using Secret_Sharing_Platform.Helper;
using Stock_Manager_Simulator_Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Stock_Manager_Simulator_Backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenService(IOptions<JwtOptions> options, IHttpContextAccessor contextAccessor)
        {
            _jwtOptions = options.Value;
            _contextAccessor = contextAccessor;
        }

        public TokenDto CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.Now.AddDays(_jwtOptions.ExpiresInDays);

            var tokenDescriptor = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Issuer,
                claims,
                expires: DateTime.Now.AddDays(_jwtOptions.ExpiresInDays),
                signingCredentials: credentials);

            return new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                Expires = expires
            };
        }

        public int GetMyId()
        {
            var myToken = new JwtSecurityTokenHandler().ReadJwtToken(
                _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString().Remove(0, 7));
            var myId = Convert.ToInt32(myToken.Claims.First(c => c.Type == "sub").Value);

            return myId;
        }
    }
}
