using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VirtualOfficeCloud.Utils.Interfaces;

namespace VirtualOfficeCloud.Utils.Implementation
{
    public class Token : IToken
    {
        private readonly IConfiguration _config;

        public Token(IConfiguration config)
        {
            _config = config;
        }

        public TokenValues CreateToken(string username, string email)
        {

            //Create token
            var claims = new[]
            {
                 //this is the subject
                 new Claim(JwtRegisteredClaimNames.Sub, email),
                 //identifier of the token
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 //some unique name, email in the user name
                 new Claim(JwtRegisteredClaimNames.UniqueName, username)
            };

            //this key is to encrypt token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:key"]));

            //here create credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Tokens:issuer"],
                _config["Tokens:audience"],
                claims,
                expires: DateTime.UtcNow.AddSeconds(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: creds);

            var results = new TokenValues
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };

            return results;

        }
    }

    public class TokenValues
        {
            public string Token { get; set; }
            public DateTime Expiration { get; set; }
        }
    }
