using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.IdentityModel.Tokens;
using MPMSRS.Models.VM;
using MPMSRS.Services.Repositories.Auth;
using RestSharp;

namespace MPMSRS.Services.Repositories.Command
{
    public class TokenMPMServices
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;

        public TokenMPMServices(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public AccessToken Authenticate(UserDto user)
        {
            try
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("id", user.EmployeeId.ToString()),
                    new Claim(ClaimTypes.Name, user.DisplayName),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes);
                string value = _tokenGenerator.GenerateToken(
                    _configuration.AccessTokenSecret,
                    _configuration.Issuer,
                    _configuration.Audience,
                    expirationTime,
                    claims);

                return new AccessToken()
                {
                    Value = value,
                    ExpirationTime = expirationTime
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
