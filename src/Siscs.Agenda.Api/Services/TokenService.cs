using System.Text;
using Siscs.Agenda.Api.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace Siscs.Agenda.Api.Services
{
    public static class TokenService
    {
        public static string GerarToken(IdentityUser usuarioAuh, ClaimsIdentity identityClaims, IList<string> roles, TokenConfig tokenConfig)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenConfig.Secret);

            if(roles.Any())
            {
                foreach(string item in roles)
                {
                    // identityClaims.AddClaim(new Claim(ClaimTypes.Name, item);
                    identityClaims.AddClaim(new Claim(ClaimTypes.Role, item));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            { 
                Subject = identityClaims,
                Issuer = tokenConfig.Emissor,
                Audience = tokenConfig.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(tokenConfig.ExpiracaoHoras),
                SigningCredentials = 
                    new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}