using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Services.Models;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Services.Helpers
{
    public static class JwtToken
    {
        public static string GenerateToken(TokenModel tokenModel)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, tokenModel.Username),
            new Claim(ClaimTypes.Role, tokenModel.Role ?? "User"),
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenModel.SigninKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenModel.Issuer,
                audience: tokenModel.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials,
                notBefore: DateTime.Now
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }


    }
}
