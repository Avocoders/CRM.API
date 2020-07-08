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

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : Controller
    {
        private readonly ILogger<LeadController> _logger;
        
        private Mapper _mapper;

        public LeadController(ILogger<LeadController> logger)
        {
            _logger = logger;
            _mapper = new Mapper();
        }

        [HttpGet]
        public ActionResult<List<LeadOutputModel>> GetLeadsAll()
        {
            LeadRepository lead = new LeadRepository();
            return Ok(_mapper.ConvertListLeadOutputModelToListLeadDTO(lead.GetAll()));
        }

        [HttpGet("{leadId}")]
        public ActionResult<LeadOutputModel> GetLeadById(long leadId)
        {
            LeadRepository lead = new LeadRepository();
            return Ok(_mapper.ConvertLeadOutputModelToLeadDTO(lead.GetById(leadId)));
        }

        //[Authorize()]
        [HttpPost]
        public ActionResult<int> PostLead(LeadInputModel leadModel)
        {
            // сделать проверку на существование email в базе
            // сделать проверку длины пароля (не менее 8 символов)
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return BadRequest("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return BadRequest("Enter the surname");
            if (string.IsNullOrWhiteSpace(leadModel.Login) && string.IsNullOrWhiteSpace(leadModel.Email)) return BadRequest("Enter a login or email");
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return BadRequest("Enter a password");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return BadRequest("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return BadRequest("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return BadRequest("Enter the date of birth");
            LeadDto leadDto = _mapper.ConvertLeadInputModelToLeadDTO(leadModel);
            LeadRepository lead = new LeadRepository();
            return Ok(lead.Add(leadDto));
        }

        [HttpPut]
        public ActionResult<LeadOutputModel> UpdateLead(LeadInputModel leadModel)
        {
            LeadRepository repo = new LeadRepository();
            if (!leadModel.Id.HasValue)
            {
                return BadRequest("ID is empty");
            }
            // сделать проверку на существование email в базе
            // сделать проверку длины пароля (не менее 8 символов)
            var leadId = repo.GetById(leadModel.Id.Value);
            if (leadId == null) return BadRequest("Lead was not found");
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return BadRequest("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return BadRequest("Enter the last name");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return BadRequest("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return BadRequest("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return BadRequest("Enter the date of birth");
            LeadDto leadDTO = _mapper.ConvertLeadInputModelToLeadDTO(leadModel);
            return Ok(_mapper.ConvertLeadOutputModelToLeadDTO(repo.Update(leadDTO)));
        }


        //[Authorize()]      
        [HttpDelete("{leadId}")]
        public ActionResult DeleteLeadById(int leadId) // поменять тип данных на long
        {
            LeadRepository repo = new LeadRepository();
            // сделать проверку на наличие Lead с передаваемым Id
            repo.Delete(leadId);
            return Ok();
        }
    }
}
