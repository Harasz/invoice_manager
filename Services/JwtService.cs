using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using invoice_manager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace invoice_manager.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;    
    
        public JwtService(IConfiguration config)    
        {    
            _config = config;    
        } 
        
        public string Generate(User user)    
        {    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            var claims = new[] {    
                new Claim(JwtRegisteredClaimNames.Sub, user.LastName),    
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())    
            };    
            
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],    
                _config["Jwt:Issuer"],    
                claims,    
                expires: DateTime.Now.AddMinutes(120),    
                signingCredentials: credentials);    
    
            return new JwtSecurityTokenHandler().WriteToken(token);    
        }
    }
}