using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.API.Models.Input;
using CRM.Data.DTO;
using CRM.Data.StoredProcedure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : Controller
    {       
        private readonly ILogger<LeadController> _logger;

        public LeadController(ILogger<LeadController> logger)
        {
            _logger = logger;
        }


        //[Authorize()]
        [HttpPost]
        public ActionResult<int> PostLead(LeadInputModel leadModel)
        {
            if (leadModel.RoleId == null) return BadRequest("Set a role");
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return BadRequest("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return BadRequest("Enter the surname");
            if (string.IsNullOrWhiteSpace(leadModel.Patronymic)) return BadRequest("Enter the patronymic");
            if (string.IsNullOrWhiteSpace(leadModel.Login)) return BadRequest("Enter a login");
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return BadRequest("Enter a password");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return BadRequest("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Email)) return BadRequest("Enter the email");
            if (leadModel.CityId == null) return BadRequest("Add a city");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return BadRequest("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.Email)) return BadRequest("Enter the email");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return BadRequest("Enter the date of birth");
            if (string.IsNullOrWhiteSpace(leadModel.RegistrationDate)) return BadRequest("Enter the registration date");            
            Mapper mapper = new Mapper();
            LeadDTO leadDTO = mapper.ConvertLeadInputModelToLeadDTO(leadModel);
            LeadCRUD lead = new LeadCRUD();
            return Ok(lead.Add(leadDTO));
        }


        //[Authorize()]
        [HttpPost("city")]
        public ActionResult<int> PostCity(CityInputModel cityModel)
        {            
            if (string.IsNullOrWhiteSpace(cityModel.Name)) return BadRequest("Enter the name of the city");            
            Mapper mapper = new Mapper();
            CityDTO cityDTO = mapper.ConvertCityInputModelToCityDTO(cityModel);
            CityCRUD lead = new CityCRUD();
            return Ok(lead.Add(cityDTO));
        }


        //[Authorize()]      
        [HttpDelete]
        public ActionResult<int> DeleteLeadById(int leadId)
        {
            LeadCRUD lead = new LeadCRUD();           
            return Ok(lead.Delete(leadId));
        }
    }
}
