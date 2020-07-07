using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

using CRM;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using CRM.API;
using CRM.API.Models.Output;
using CRM.API.Models.Input;
using CRM.Data.DTO;
using CRM.Data.StoredProcedure;

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
                return BadRequest("Введена неверная пара логин-пароль");
            }
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            Mapper mapper = new Mapper();
            LeadCRUD leadCRUD = new LeadCRUD();
            LeadDTO leadDTO = leadCRUD.GetByLogin(login);
            LeadOutputModel lead = mapper.ConvertLeadOutputModelToLeadDTO(leadDTO);

            if (lead != null)
            {
                if (lead.Password == password)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,lead.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,lead.Role)
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
