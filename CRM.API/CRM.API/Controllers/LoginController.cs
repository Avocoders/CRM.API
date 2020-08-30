using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CRM.API.Models.Input;
using CRM.API.Sha256;
using CRM.Data.DTO;
using CRM.Data;
using System.Threading.Tasks;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILeadRepository _repo;

        public LoginController(ILeadRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> Authorization([FromBody] AuthorizeInputModel auth)
        {
            ClaimsIdentity identity = await GetIdentity(auth.Login, auth.Password);
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

        private async ValueTask<ClaimsIdentity> GetIdentity(string login, string password)
        {
            DataWrapper<AuthorizationDto> authorizationDto = await _repo.GetByLogin(login);
            PasswordEncryptor encryptor = new PasswordEncryptor();

            if (authorizationDto != null)
            {
                if (encryptor.CheckPassword(authorizationDto.Data.Password, password))
                {
                    List<Claim> claims = new List<Claim>()
                    {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,authorizationDto.Data.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,authorizationDto.Data.Role.Name)
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
