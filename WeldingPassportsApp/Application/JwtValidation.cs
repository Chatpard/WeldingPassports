using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application
{
    public static class JwtValidation
    {
        public static string GenerateToken(int userID, IConfiguration config)
        {
            var myIssuer = GetMyIssuer(config);
            var myAudience = GetMyAudience(config);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, userID.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(GetMySecurityKey(config), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static bool ValidateCurrentToken(string token, IConfiguration config)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = GetMyIssuer(config),
                    ValidAudience = GetMyAudience(config),
                    IssuerSigningKey = GetMySecurityKey(config)
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private static SecurityKey GetMySecurityKey(IConfiguration config)
        {
            var mySecret = config["JWTSecurityKey"];
            
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
        }

        private static string GetMyAudience(IConfiguration config)
        {
            return config["JWTAudience"];
        }

        private static string GetMyIssuer(IConfiguration config)
        {
            return config["JWTIssuer"];
        }
    }
}
