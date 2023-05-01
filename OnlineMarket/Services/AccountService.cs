using Microsoft.IdentityModel.Tokens;
using OnlineMarket.Models.Options;
using OnlineMarket.Models.Repository;
using OnlineMarket.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineMarket.Services
{
    public class AccountService
    {
        private readonly UserService m_Service;

        public AccountService(UserService service)
        {
            m_Service = service;
        }
        public async Task<TokenData> Token(string email, string password)
        {
            var identity = await GetIdentity(email, password);
            if (identity == null) return new TokenData() { Message = "Invalid email or password." };
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(
            AuthOptions.GetSymmetricSecurityKey(),
            SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new TokenData() { AccessToken = encodedJwt };
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            User user = await m_Service.GetUserByEmail(email);
            if (user == null) return null; // если пользователя не найдено
            if (!user.Pwd.Equals(password)) return null;
            var claims = new List<Claim> {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Id % 2 == 0 ? "User" : "Admin" )};
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
            claims,
            "Token",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType
            );
            return claimsIdentity;
        }
    }
}
