using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CRM.API.Models.Input;
using CRM.Data.DTO;
using CRM.Data;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult Auth([FromBody] AuthorizeInputModel auth)
        {
            ClaimsIdentity identity = GetIdentity(auth.Login, auth.Password);
            if (identity != null)
            {
                DateTime now = DateTime.UtcNow;
                JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: Models.TokenOptions.ISSUER,
                    audience: Models.TokenOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(Models.TokenOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(Models.TokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

                var response = new
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(jwt),
                    login = auth.Login
                };
                return Ok(response);
            }
            else
            {
                return BadRequest("Invalid login-password pair entered"); 
            }
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            LeadRepository leadRepository = new LeadRepository();
            LeadDto leadDto = leadRepository.GetByLogin(login);

            if (leadDto != null)
            {
                if (leadDto.Password == password)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,leadDto.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,leadDto.Role)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
