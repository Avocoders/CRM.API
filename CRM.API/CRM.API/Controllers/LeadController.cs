using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.Data.DTO;
using CRM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : Controller
    {
        private readonly ILogger<LeadController> _logger;
        
        private Mapper _mapper;
        string pattern = "^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z])";

        public LeadController(ILogger<LeadController> logger)
        {
            _logger = logger;
            _mapper = new Mapper();
        }

        //[Authorize()]
        [HttpGet]
        public ActionResult<List<LeadOutputModel>> GetLeadsAll()
        {
            LeadRepository repo = new LeadRepository();
            return Ok(_mapper.ConvertListLeadOutputModelToListLeadDTO(repo.GetAll()));
        }

        //[Authorize()]
        [HttpGet("{leadId}")]
        public ActionResult<LeadOutputModel> GetLeadById(long leadId)
        {
            LeadRepository repo = new LeadRepository();
            return Ok(_mapper.ConvertLeadOutputModelToLeadDTO(repo.GetById(leadId)));
        }

        //[Authorize()]
        [HttpPost]
        public ActionResult<int> PostLead(LeadInputModel leadModel)
        {
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return BadRequest("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return BadRequest("Enter the last name");            
            if (string.IsNullOrWhiteSpace(leadModel.Login) && string.IsNullOrWhiteSpace(leadModel.Email)) return BadRequest("Enter a login or the email");
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return BadRequest("Enter a password");
            if (leadModel.Password.Length <= 8 || !Regex.IsMatch(leadModel.Password, pattern)) return BadRequest("Password have to be at least 8 signs long and contain lowercase, uppercase and number");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return BadRequest("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return BadRequest("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return BadRequest("Enter the date of birth");
            LeadDto leadDto = _mapper.ConvertLeadInputModelToLeadDTO(leadModel);
            LeadRepository repo = new LeadRepository();
            if (!string.IsNullOrWhiteSpace(leadModel.Login) && repo.FindLeadByLogin(leadModel.Login) != 0) return BadRequest("User with this login already exists");
            if (!string.IsNullOrWhiteSpace(leadModel.Email) && repo.FindLeadByEmail(leadModel.Email) != 0) return BadRequest("User with this email already exists");
            return Ok(repo.Add(leadDto));
        }

        //[Authorize()]
        [HttpPut]
        public ActionResult<LeadOutputModel> UpdateLead(LeadInputModel leadModel)
        {
            LeadRepository repo = new LeadRepository();
            if (!leadModel.Id.HasValue)
            {
                return BadRequest("ID is empty");
            }     
            var leadId = repo.GetById(leadModel.Id.Value);
            if (leadId == null) return BadRequest("Lead was not found");
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return BadRequest("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return BadRequest("Enter the last name");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return BadRequest("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return BadRequest("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return BadRequest("Enter the date of birth");
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return BadRequest("Enter a password");
            if (leadModel.Password.Length <= 8 || !Regex.IsMatch(leadModel.Password, pattern)) return BadRequest("Password have to be at least 8 signs long and contain lowercase, uppercase and number");
            LeadDto leadDTO = _mapper.ConvertLeadInputModelToLeadDTO(leadModel);
            return Ok(_mapper.ConvertLeadOutputModelToLeadDTO(repo.Update(leadDTO)));
        }

        //[Authorize()]      
        [HttpDelete("{leadId}")]
        public ActionResult DeleteLeadById(long leadId) 
        {
            LeadRepository repo = new LeadRepository();            
            if (repo.GetById(leadId).Id == null) return BadRequest("Lead was not found");
            repo.Delete(leadId);
            return Ok();
        }

        //[Authorize()]
        [HttpPatch]
        public ActionResult<string> UpdateEmailByLeadId(EmailInputModel emailModel)
        {
            LeadRepository repo = new LeadRepository();  
            if (emailModel.Id == null) return BadRequest("Lead was not found");
            if (!string.IsNullOrWhiteSpace(emailModel.Email) && repo.FindLeadByEmail(emailModel.Email) != 0) return BadRequest("User with this email already exists");
            return Ok(repo.UpdateEmailByLeadId(emailModel.Id, emailModel.Email));
        }
    }
}
