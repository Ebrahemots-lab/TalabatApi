using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Entites.Identity;
using Api.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;
        private readonly UserManager<AppUser> userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            this.config = config;
            this.userManager = userManager;
        }
        public async Task<string> GenerateToken(AppUser user)
        {
            //Generate Token
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name,user.UserName),
                new(ClaimTypes.Email,user.Email),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };

            //adding roles 
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //this will create the key that will be used to signature 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:key"]));

            var generatedToken = new JwtSecurityToken
            (
                issuer: config["JWT:issuer"],
                audience: config["JWT:audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256) // this will create the signature
            );

            var token = new JwtSecurityTokenHandler();
            return token.WriteToken(generatedToken);

        }
    }
}