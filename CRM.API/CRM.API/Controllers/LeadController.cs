using System.Collections.Generic;
using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.API.Sha256;
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
        
        private readonly Mapper _mapper;
        
        private readonly LeadRepository _repo;

        string patternPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])([a-zA-Z0-9@#$%^&+=*.\-_]){8,20}$";
        string patternLogin = @"^((?!.*@.*\..*$))([a-zA-Z0-9@#$%^&+=*.\-_]){6,}$";
        string patternEmail = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public LeadController(ILogger<LeadController> logger)
        {
            _logger = logger;
            _mapper = new Mapper();
            _repo = new LeadRepository();
        }

        //[Authorize()]
        [HttpGet]
        public ActionResult<List<LeadOutputModel>> GetLeadsAll()
        {
            return Ok(_mapper.ConvertLeadDtosToLeadOutputModels(_repo.GetAll()));
        }

        //[Authorize()]
        [HttpGet("{leadId}")]
        public ActionResult<LeadOutputModel> GetLeadById(long leadId)
        {
            return Ok(_mapper.ConvertLeadDtoToLeadOutputModel(_repo.GetById(leadId)));
        }

        //[Authorize()]
        [HttpPost]
        public ActionResult<int> PostLead(LeadInputModel leadModel)
        {
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return BadRequest("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return BadRequest("Enter the last name");            
            if (string.IsNullOrWhiteSpace(leadModel.Login) && string.IsNullOrWhiteSpace(leadModel.Email)) return BadRequest("Enter a login or the email");
            if (!string.IsNullOrWhiteSpace(leadModel.Login))
            {
                if(_repo.FindLeadByLogin(leadModel.Login) != 0) return BadRequest("User with this login already exists");
                if (!Regex.IsMatch(leadModel.Login, patternLogin)) return BadRequest("The Login is incorrect");
            }
            if (!string.IsNullOrWhiteSpace(leadModel.Email))
            {
                if(_repo.FindLeadByEmail(leadModel.Email) != 0) return BadRequest("User with this email already exists");
                if((!Regex.IsMatch(leadModel.Email, patternEmail))) return BadRequest("The Email is incorrect");
            }
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return BadRequest("Enter a password");
            if (!Regex.IsMatch(leadModel.Password, patternPassword)) return BadRequest("Password have to be between 8 and 20 characters long and contain lowercase, uppercase and number, possible characters: @#$%^&+=*.-_");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return BadRequest("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return BadRequest("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return BadRequest("Enter the date of birth");
            leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);
            LeadDto leadDto = _mapper.ConvertLeadInputModelToLeadDTO(leadModel);            
            return Ok(_repo.Add(leadDto));
        }

        //[Authorize()]
        [HttpPut]
        public ActionResult<LeadOutputModel> UpdateLead(LeadInputModel leadModel)
        {
            if (!leadModel.Id.HasValue)
            {
                return BadRequest("ID is empty");
            }     
            var leadId = _repo.GetById(leadModel.Id.Value);
            if (leadId == null) return BadRequest("Lead was not found");
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return BadRequest("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return BadRequest("Enter the last name");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return BadRequest("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return BadRequest("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return BadRequest("Enter the date of birth");
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return BadRequest("Enter a password");
            if (!Regex.IsMatch(leadModel.Password, patternPassword)) return BadRequest("Password have to be between 8 and 20 characters long and contain lowercase, uppercase and number, possible characters: @#$%^&+=*.-_");
            LeadDto leadDTO = _mapper.ConvertLeadInputModelToLeadDTO(leadModel);
            return Ok(_repo.Update(leadDTO));
        }

        //[Authorize()]      
        [HttpDelete("{leadId}")]
        public ActionResult DeleteLeadById(long leadId) 
        {
            if (_repo.GetById(leadId).Id == null) return BadRequest("Lead was not found");
            _repo.Delete(leadId);
            return Ok();
        }

        //[Authorize()]
        [HttpPatch]
        public ActionResult<string> UpdateEmailByLeadId(EmailInputModel emailModel)
        {
            if (emailModel.Id == null) return BadRequest("Lead was not found");
            if (string.IsNullOrWhiteSpace(emailModel.Email))
            {
                return BadRequest("Enter the email");
            }
            else            
            {
                if (_repo.FindLeadByEmail(emailModel.Email) != 0) return BadRequest("User with this email already exists");
                if ((!Regex.IsMatch(emailModel.Email, patternEmail))) return BadRequest("The Email is incorrect");
            }
            return Ok(_repo.UpdateEmailByLeadId(emailModel.Id, emailModel.Email));
        }
    }
}
